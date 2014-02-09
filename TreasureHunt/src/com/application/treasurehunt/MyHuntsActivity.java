package com.application.treasurehunt;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import sqlLiteDatabase.Hunt;
import sqlLiteDatabase.HuntDAO;

import com.application.treasurehunt.RegisterWithHuntActivity.GetHuntDescriptionTask;

import Utilities.ExpandableListAdapter;
import Utilities.JSONParser;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.app.ActionBar;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ExpandableListView;
import android.widget.ExpandableListView.OnChildClickListener;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

public class MyHuntsActivity extends Activity {

	ExpandableListView mListView;
	List<String> listDataHeader;
	//http://www.androidhive.info/2013/07/android-expandable-list-view-tutorial/
	HashMap<String, List<String>> listDataChild;
	
	ReturnUserHuntsTask mReturnUserHuntsTask;
	
	public JSONParser jsonParser = new JSONParser();
	private static final String returnUserHuntsUrl =  "http://192.168.1.74:80/webservice/chooseUserHunt.php";
	private static final String tagSuccess = "success";
	private static final String tagMessage = "message";
	private static JSONArray tagResult;
	
	SharedPreferences.Editor editor;
	SharedPreferences settings;
	
	private ProgressDialog pDialog;
	
	int currentUserId;
	
	private HuntDAO huntDataSource;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_my_hunts);
		
		huntDataSource = new HuntDAO(this);
		huntDataSource.open();
		
		if(Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB)
		{
			ActionBar actionBar = getActionBar();
			actionBar.setTitle("Treasure Hunt");
			actionBar.setSubtitle("My hunts");
		}
		
		mListView = (ExpandableListView) findViewById(R.id.list_of_user_hunts_id);
		
		settings = getSharedPreferences("UserPreferencesFile", 0);
		editor = settings.edit();
		
		currentUserId = settings.getInt("currentUserId", 0);
	        
        listDataHeader = new ArrayList<String>();
        listDataChild = new HashMap<String, List<String>>();
		
		attemptToReturnUserHunts();
	}
	
	private void attemptToReturnUserHunts()
	{
		if (mReturnUserHuntsTask != null) {
			return;
		} 	
		mReturnUserHuntsTask = new ReturnUserHuntsTask();
		mReturnUserHuntsTask.execute((String) null);

		Handler handlerForUserTask = new Handler();
		handlerForUserTask.postDelayed(new Runnable()
		{
			@Override
			public void run()
			{
				if(mReturnUserHuntsTask!= null)
				{
					if(mReturnUserHuntsTask.getStatus() == AsyncTask.Status.RUNNING)
					{
						mReturnUserHuntsTask.cancel(true);
						Toast.makeText(MyHuntsActivity.this, "Connection timeout. Please try again.", Toast.LENGTH_LONG).show();
					}
				}
			}
		}
		, 10000);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		super.onCreateOptionsMenu(menu);
		getMenuInflater().inflate(R.menu.login, menu);
		menu.add(Menu.NONE, 1, Menu.NONE, "Log out");
		return true;
	} 
	
	//http://mobileorchard.com/android-app-development-menus-part-1-options-menu/
	@Override
	public boolean onOptionsItemSelected(MenuItem item)
	{
		switch(item.getItemId())
		{
		case 1:
			SharedPreferences settings = getSharedPreferences("UserPreferencesFile", 0);
			SharedPreferences.Editor editor = settings.edit();
			editor.clear();
			editor.commit();
			Intent loginActivityIntent = new Intent(MyHuntsActivity.this, LoginActivity.class);
			startActivity(loginActivityIntent);
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	public class ReturnUserHuntsTask extends AsyncTask<String, String, String> {
		
		@Override
		protected void onPreExecute()
		{
			super.onPreExecute();
			pDialog = new ProgressDialog(MyHuntsActivity.this);
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
				parameters.add(new BasicNameValuePair("userId", Integer.toString(currentUserId)));
				
				JSONObject json = jsonParser.makeHttpRequest(returnUserHuntsUrl, "POST", parameters);
				
				Log.d("Get hunts attempt", json.toString());
				
				success = json.getInt(tagSuccess);
				if(success == 1)
				{
					Log.d("Returning of hunts successful!", json.toString());
					tagResult = json.getJSONArray("results");
					
					//-http://stackoverflow.com/questions/8411154/null-pointer-exception-while-inserting-json-array-into-sqlite-database
					for(int i=0; i < tagResult.length(); i++)
					{
						huntDataSource.addUserHunt(tagResult.getJSONArray(i).getJSONObject(0).getString("HuntName"));
					}
					
					return tagMessage.toString();
				}
				else
				{
					Log.d("Returning of hunts failed! ", json.getString(tagMessage));
					
					return json.getString(tagMessage);
				}
				
			} catch (JSONException e) {
				Log.d("Leaderboard", e.toString());
			}

			return null;
		}

		@Override
		protected void onPostExecute(final String fileUrl) {
			mReturnUserHuntsTask = null;
			pDialog.dismiss();

			if (fileUrl != null) 
			{	
				List<Hunt> listOfHunts = huntDataSource.getAllUserHunts();
				
				//For every listOfHunts, add the header with its name
				for(int i=0; i< listOfHunts.size(); i++)
				{
					listDataHeader.add(listOfHunts.get(i).getHuntName().toString());
					List<String> huntOptions = new ArrayList<String>();
					huntOptions.add("Continue");
					huntOptions.add("Stats");
					
					listDataChild.put(listDataHeader.get(i), huntOptions);		
				}
				
		        
				final ExpandableListAdapter adapter = new ExpandableListAdapter(MyHuntsActivity.this, listDataHeader, listDataChild);
				mListView.setAdapter(adapter);	
				
				mListView.setOnChildClickListener(new OnChildClickListener()
				{

					@Override
					public boolean onChildClick(ExpandableListView parent,
							View v, int groupPosition, int childPosition,
							long id) {
						
						//check here to make sure that user hasn't already registered with this hunt
						//http://stackoverflow.com/questions/4508979/android-listview-get-selected-item
						String selectedHunt = listDataHeader.get(groupPosition);
					    
						//Instead of starting a new activity, it only starts if the stats or map button pressed
						//Intent registerWithHuntintent = new Intent(MyHuntsActivity.this, RegisterWithHuntActivity.class);
						editor.putString("currentHuntName", selectedHunt);
						editor.commit(); 
						
						if(childPosition == 0)
						{
							Intent registerWithHuntIntent = new Intent(MyHuntsActivity.this, RegisterWithHuntActivity.class);
							startActivity(registerWithHuntIntent);
						}
						else
						{
							Intent statsActivityIntent = new Intent(MyHuntsActivity.this, StatsActivity.class);
							startActivity(statsActivityIntent);
						}
						
						//If the Continue is clicked then go to scan page
						//If the stats is clicked then go to the stats page
						return false;
					}
					
				});
				
				//http://stackoverflow.com/questions/16189651/android-listview-selected-item-stay-highlighted
				mListView.setOnItemClickListener(new OnItemClickListener() {
					public void onItemClick(AdapterView<?> parent, View view,
							int position, long id) {
						
						//check here to make sure that user hasn't already registered with this hunt
						//http://stackoverflow.com/questions/4508979/android-listview-get-selected-item
						Hunt selectedHunt = (Hunt) ((Menu) adapter).getItem(position);
					    
						//Instead of starting a new activity, it only starts if the stats or map button pressed
						//Intent registerWithHuntintent = new Intent(MyHuntsActivity.this, RegisterWithHuntActivity.class);
						editor.putString("currentHuntName", selectedHunt.getHuntName());
						editor.commit(); 

						//startActivity(registerWithHuntintent);
					}
				});
				
			} 
			else 
			{
				Toast.makeText(MyHuntsActivity.this, "Nothing returned from the database", Toast.LENGTH_LONG).show();
			}
		}

		@Override
		protected void onCancelled() {
			mReturnUserHuntsTask = null;
		}
	}

}
