package com.application.treasurehunt;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.application.treasurehunt.LoginActivity.UserLoginTask;

import Utilities.JSONParser;
import android.os.AsyncTask;
import android.os.Bundle;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

public class ChooseHuntActivity extends Activity {

	JSONParser jsonParser = new JSONParser();
	private static final String myLoginUrl =  "http://192.168.1.74:80/webservice/choosehunt.php";
	private static final String tagSuccess = "success";
	private static final String tagMessage = "message";
	private static JSONArray tagResult;
	
	private ReturnHuntsTask mAuthTask = null;
	private ProgressDialog pDialog; //spinner thing
	
	private ListView mListView;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_choose_hunt);
		
		mListView = (ListView) findViewById(R.id.hunt_list_view);
			
		findViewById(R.id.find_hunts_button).setOnClickListener(
				new View.OnClickListener() {
					@Override
					public void onClick(View view) {
						attemptReturnHunts();
					}
				});
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.choose_hunt, menu);
		return true;
	}
	
	public void attemptReturnHunts() {
		if (mAuthTask != null) {
			return;
		} 			
			mAuthTask = new ReturnHuntsTask(); // Do ASYNC way
			mAuthTask.execute((String) null);		
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
		
		int success;
		
		try {
			
			Log.d("request", "starting");
			List<NameValuePair> parameters = new ArrayList<NameValuePair>();
			JSONObject json = jsonParser.makeHttpRequest(myLoginUrl, "GET", parameters);
			Log.d("Get hunts attempt", json.toString());
			
			success = json.getInt(tagSuccess);
			if(success == 1)
			{
				Log.d("Returning of hunts successful!", json.toString());
				finish(); 
				tagResult = json.getJSONArray("result");
				return tagResult.toString();
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

		if (fileUrl != null) {
			Toast.makeText(ChooseHuntActivity.this, fileUrl, Toast.LENGTH_LONG).show();
		} else {
			Toast.makeText(ChooseHuntActivity.this, "Nothing returned from the database", Toast.LENGTH_LONG).show();
		}
	}

	@Override
	protected void onCancelled() {
		mAuthTask = null;
	}
}
}

