package com.application.treasurehunt;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ExecutionException;

import org.apache.http.NameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import sqlLiteDatabase.Hunt;
import sqlLiteDatabase.HuntDAO;

import Utilities.JSONParser;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.preference.PreferenceManager;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;
//http://net.tutsplus.com/tutorials/php/php-database-access-are-you-doing-it-correctly/
public class ChooseHuntActivity extends Activity implements OnItemClickListener {

	public JSONParser jsonParser = new JSONParser();
	private static final String myChooseHuntUrl =  "http://192.168.1.74:80/webservice/choosehunt.php";
	//private static final String myChooseHuntUrl =  "http://143.117.190.106:80/webservice/choosehunt.php";
	private static final String tagSuccess = "success";
	private static final String tagMessage = "message";
	private static JSONArray tagResult;
	
	private ReturnHuntsTask mAuthTask = null;
	private ProgressDialog pDialog; 
	
	private HuntDAO huntDataSource;
	
	private ListView mListView;
	String currentUser;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_choose_hunt);
		huntDataSource = new HuntDAO(this);
		huntDataSource.open();
		
		mListView = (ListView) findViewById(R.id.hunt_list_view);		
		
		Intent intent = getIntent();
		currentUser = intent.getStringExtra(getString(R.string.email_label));
		
		attemptReturnHunts();
		
		/*findViewById(R.id.sign_in_button).setOnClickListener(
				new View.OnClickListener() {
					@Override
					public void onClick(View view) {
						updateListOfHunts();
					}
				}); */
	}

	/*@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.choose_hunt, menu);
		return true;
	} */
	
//	@Override
//	protected void onResume()
//	{
//		//super.onResume();
//		//updateListOfHunts();
//	}
	
	private void updateListOfHunts()
	{
		//get this to happen automatically at some point // refreshes itself
		huntDataSource.updateDatabaseLocally();
		attemptReturnHunts();
	}
	
	private void attemptReturnHunts() {
		if (mAuthTask != null) {
			return;
		} 			
			mAuthTask = new ReturnHuntsTask(); // Do ASYNC way
			mAuthTask.execute((String) null);	
			//http://stackoverflow.com/questions/7882739/android-setting-a-timeout-for-an-asynctask?rq=1
			Handler handler = new Handler();
			handler.postDelayed(new Runnable()
			{
				@Override
				public void run()
				{
					if(mAuthTask != null)
					{
						if(mAuthTask.getStatus() == AsyncTask.Status.RUNNING)
						{
							mAuthTask.cancel(true);
							pDialog.cancel();
							Toast.makeText(ChooseHuntActivity.this, "Connection timeout. Please try again.", Toast.LENGTH_LONG).show();
						}
					}
				}
			}
			
			, 10000);
			try {
				mAuthTask.get();
			} catch (InterruptedException e) {
				e.printStackTrace();
			} catch (ExecutionException e) {
				e.printStackTrace();
			}
	}


public class ReturnHuntsTask extends AsyncTask<String, String, String> {
	
	@Override
	protected void onPreExecute()
	{
		super.onPreExecute();
		pDialog = new ProgressDialog(ChooseHuntActivity.this);
        pDialog.setMessage("Attempting to get treasure hunts...");
		pDialog.setIndeterminate(false);
		pDialog.setCancelable(true);
		pDialog.show();
	}
	
	@Override
	protected String doInBackground(String... args) {
		//http://www.mybringback.com/tutorial-series/13193/android-mysql-php-json-part-5-developing-the-android-application/
		//http://www.php.net/manual/en/pdostatement.fetchall.php
		//http://stackoverflow.com/questions/14491430/using-pdo-to-echo-display-all-rows-from-a-table
	
		int success;
		
		try {
			
			Log.d("request", "starting");
			List<NameValuePair> parameters = new ArrayList<NameValuePair>();
			JSONObject json = jsonParser.makeHttpRequest(myChooseHuntUrl, "GET", parameters);
			
			Log.d("Get hunts attempt", json.toString());
			
			success = json.getInt(tagSuccess);
			if(success == 1)
			{
				Log.d("Returning of hunts successful!", json.toString());
				tagResult = json.getJSONArray("results");
				
				//-http://stackoverflow.com/questions/8411154/null-pointer-exception-while-inserting-json-array-into-sqlite-database
				for(int i=0; i < tagResult.length(); i++)
				{
					huntDataSource.addHunt(tagResult.getJSONObject(i).getString("HuntName"));
				}
				
				return tagMessage.toString();
			}
			else
			{
				Log.d("Returning of hunts failed! ", json.getString(tagMessage));
				
				return json.getString(tagMessage);
			}
			
		} catch (JSONException e) {
		
		}

		return null;
	}

	@Override
	protected void onPostExecute(final String fileUrl) {
		mAuthTask = null;
		pDialog.dismiss();

		if (fileUrl != null) 
		{	
			List<Hunt> listOfHunts = huntDataSource.getAllHunts();
			final ArrayAdapter<Hunt> adapter = new ArrayAdapter<Hunt>(ChooseHuntActivity.this, android.R.layout.simple_list_item_1, listOfHunts);
			mListView.setAdapter(adapter);	
			
			//http://stackoverflow.com/questions/16189651/android-listview-selected-item-stay-highlighted
			mListView.setOnItemClickListener(new OnItemClickListener() {
				public void onItemClick(AdapterView<?> parent, View view,
						int position, long id) {
					
					//check here to make sure that user hasn't already registered with this hunt
					//http://stackoverflow.com/questions/4508979/android-listview-get-selected-item
					Hunt selectedHunt = adapter.getItem(position);
				    
					Intent registerWithHuntintent = new Intent(ChooseHuntActivity.this, RegisterWithHuntActivity.class);
					registerWithHuntintent.putExtra("Email", currentUser);
					registerWithHuntintent.putExtra("Current hunt", selectedHunt.getHuntName());
					startActivity(registerWithHuntintent);
				}
			});
			
		} 
		else 
		{
			Toast.makeText(ChooseHuntActivity.this, "Nothing returned from the database", Toast.LENGTH_LONG).show();
		}
	}

	@Override
	protected void onCancelled() {
		mAuthTask = null;
	}
}


@Override
public void onItemClick(AdapterView<?> arg0, View arg1, int arg2, long arg3) {
	// TODO Auto-generated method stub
	
}
}

