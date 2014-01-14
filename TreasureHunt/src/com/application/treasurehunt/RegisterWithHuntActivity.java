package com.application.treasurehunt;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.application.treasurehunt.RegisterActivity.UserRegisterTask;

import Utilities.JSONParser;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.text.TextUtils;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

public class RegisterWithHuntActivity extends Activity{
	
	private String mPassword;
	private EditText mPasswordView;
	
	private static final String getUserIdUrl =  "http://192.168.1.74:80/webservice/returnCurrentUserId.php";
	private static final String getHuntIdUrl =  "http://192.168.1.74:80/webservice/returnCurrentHuntId.php";
	private static final String registerUserWithHuntUrl =  "http://192.168.1.74:80/webservice/huntParticipantSave.php";
	
	private static final String tagSuccess = "success";
	private static final String tagMessage = "message";
	
	public JSONParser jsonParserForRegister = new JSONParser();
	
	private static JSONObject userIdResult;
	private static JSONObject huntIdResult;
	
	private UserRegisterWithHuntTask mAuthTask = null;
	private GetHuntIdTask mHuntTask = null;
	private GetUserIdTask mUserTask = null;
	private ProgressDialog pDialog;
	
	String currentUser;
	String currentHunt;
	
	boolean currentHuntIdReturned = false;
	boolean currentUserIdReturned = false;
	
	int userId = 0;
	int huntId = 0;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_register_with_hunt);
		
		mPasswordView = (EditText) findViewById(R.id.register_hunt_password);
		
		attemptToReturnHuntId();
		attemptToReturnUserId();
		
		findViewById(R.id.register_hunt_button).setOnClickListener(
				new View.OnClickListener() {
					@Override
					public void onClick(View view) {

						
						if(currentUserIdReturned && currentHuntIdReturned){
							attemptRegisterWithHunt();
						}
						else
						{
							Toast.makeText(RegisterWithHuntActivity.this, "Internal error", Toast.LENGTH_LONG).show();
						}
						
					}
				});
		
		Intent intent = getIntent();
		currentUser = intent.getStringExtra("Email");
		currentHunt = intent.getStringExtra("Current hunt");
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.register_with_hunt, menu);
		return true;
	}
	
	private void attemptToReturnUserId()
	{
		if (mUserTask != null) {
			return;
		} 	
		mUserTask = new GetUserIdTask();
		mUserTask.execute((String) null);

		Handler handlerForUserTask = new Handler();
		handlerForUserTask.postDelayed(new Runnable()
		{
			@Override
			public void run()
			{
				if(mUserTask!= null)
				{
					if(mUserTask.getStatus() == AsyncTask.Status.RUNNING)
					{
						mUserTask.cancel(true);
						pDialog.cancel();
						Toast.makeText(RegisterWithHuntActivity.this, "Connection timeout. Please try again.", Toast.LENGTH_LONG).show();
					}
				}
			}
		}
		, 10000);	
	}
	
	private void attemptToReturnHuntId()
	{	
		if (mHuntTask != null) {
			return;
		} 	
		
		mHuntTask = new GetHuntIdTask();
		mHuntTask.execute((String) null);
		
		Handler handlerForHuntTask = new Handler();
		handlerForHuntTask.postDelayed(new Runnable()
		{
			@Override
			public void run()
			{
				if(mHuntTask!= null)
				{
					if(mHuntTask.getStatus() == AsyncTask.Status.RUNNING)
					{
						mHuntTask.cancel(true);
						pDialog.cancel();
						Toast.makeText(RegisterWithHuntActivity.this, "Connection timeout. Please try again.", Toast.LENGTH_LONG).show();
					}
				}
			}
		}
		, 100000);	
		
	}
	
	private void attemptRegisterWithHunt() {	
		if (mAuthTask != null) {
			return;
		} 		
		mPasswordView.setError(null);
		mPassword = mPasswordView.getText().toString();
		
		if(isValidPassword())
		{			
			mAuthTask = new UserRegisterWithHuntTask(); // Do ASYNC way
			mAuthTask.execute((String) null);
			
			//http://stackoverflow.com/questions/7882739/android-setting-a-timeout-for-an-asynctask?rq=1
			Handler handlerForMAuth = new Handler();
			handlerForMAuth.postDelayed(new Runnable()
			{
				@Override
				public void run()
				{
					if(mAuthTask!= null)
					{
						if(mAuthTask.getStatus() == AsyncTask.Status.RUNNING)
						{
							mAuthTask.cancel(true);
							pDialog.cancel();
							Toast.makeText(RegisterWithHuntActivity.this, "Connection timeout. Please try again.", Toast.LENGTH_LONG).show();
						}
					}
				}
			}
			, 100000);			
		}	
	}
	
	private boolean isValidPassword()
	{
		if (TextUtils.isEmpty(mPassword)) {
			mPasswordView.setError(getString(R.string.error_password_null));
			return false;	
		}	
		return true;		
	}
	
public class GetUserIdTask extends AsyncTask<String, String, String> {

	@Override
	protected String doInBackground(String... arg0) {
		
		int success;
		//GETTING THE USER ID
		List<NameValuePair> parametersForUserId = new ArrayList<NameValuePair>();
		//http://stackoverflow.com/questions/8603583/sending-integer-to-http-server-using-namevaluepair
		parametersForUserId.add(new BasicNameValuePair("email", currentUser));
		
		try{
			Log.d("request", "starting");
			JSONObject jsonFindUserId = jsonParserForRegister.makeHttpRequest(getUserIdUrl, "POST", parametersForUserId);
			Log.d("Get User Id Attempt", jsonFindUserId.toString());
			success = jsonFindUserId.getInt(tagSuccess);
			
			if(success == 1)
			{
				userIdResult = jsonFindUserId.getJSONObject("result");
				currentUserIdReturned = true;
				userId = userIdResult.getInt("UserId");
				return jsonFindUserId.getString(tagMessage);
				
			}
			else
			{
				Log.d("Getting User Id failed!", jsonFindUserId.getString(tagMessage));
				return jsonFindUserId.getString(tagMessage);
			}
		}
		catch (JSONException e) {
			
		}
		
		return null;
	}
	
	@Override
	protected void onPostExecute(final String fileUrl) {
		mUserTask = null;

		if (fileUrl != null) {
			//Toast.makeText(RegisterWithHuntActivity.this, fileUrl, Toast.LENGTH_LONG).show();
		} else {
			Toast.makeText(RegisterWithHuntActivity.this, "Nothing returned from the database", Toast.LENGTH_LONG).show();
		}
	}

	@Override
	protected void onCancelled() {
		mUserTask = null;
	}
}

public class GetHuntIdTask extends AsyncTask<String, String, String> {

	@Override
	protected String doInBackground(String... arg0) {
		int huntIdSuccess;		
		
		try {
					
			//GETTING THE HUNT ID
			List<NameValuePair> parametersForHuntId = new ArrayList<NameValuePair>();
			//http://stackoverflow.com/questions/8603583/sending-integer-to-http-server-using-namevaluepair
			parametersForHuntId.add(new BasicNameValuePair("hunt", currentHunt));
			
			Log.d("request", "starting");
			JSONObject jsonFindHuntId = jsonParserForRegister.makeHttpRequest(getHuntIdUrl, "POST", parametersForHuntId);
			Log.d("Register With Hunt Attempt", jsonFindHuntId.toString());
			huntIdSuccess = jsonFindHuntId.getInt(tagSuccess);
			if(huntIdSuccess == 1)
			{
				huntIdResult = jsonFindHuntId.getJSONObject("result");
				currentHuntIdReturned = true;
				huntId = huntIdResult.getInt("HuntId");
				return jsonFindHuntId.getString(tagMessage);			
			}
			else
			{
				Log.d("Getting Hunt Id failed!", jsonFindHuntId.getString(tagMessage));
				return jsonFindHuntId.getString(tagMessage);
			}
		}catch (JSONException e) {
			
			}
		return null;	
	}

	@Override
	protected void onPostExecute(final String fileUrl) {
		mHuntTask = null;

		if (fileUrl != null) {
			//Toast.makeText(RegisterWithHuntActivity.this, fileUrl, Toast.LENGTH_LONG).show();
		} else {
			Toast.makeText(RegisterWithHuntActivity.this, "Nothing returned from the database", Toast.LENGTH_LONG).show();
		}
	}

	@Override
	protected void onCancelled() {
		mHuntTask = null;
	}
}
	
public class UserRegisterWithHuntTask extends AsyncTask<String, String, String> {
		
		@Override
		protected void onPreExecute()
		{
			super.onPreExecute();
			pDialog = new ProgressDialog(RegisterWithHuntActivity.this);
            pDialog.setMessage("Attempting register...");
			pDialog.setIndeterminate(false);
			pDialog.setCancelable(true);
			pDialog.show();
		}
		
		@Override
		protected String doInBackground(String... args) {
			//http://www.mybringback.com/tutorial-series/13193/android-mysql-php-json-part-5-developing-the-android-application/
			
			int success;
			String password = mPassword;
				//Check to make sure password is correct
				List<NameValuePair> parametersForPasswordCheck = new ArrayList<NameValuePair>();
				
				//http://stackoverflow.com/questions/8603583/sending-integer-to-http-server-using-namevaluepair
				parametersForPasswordCheck.add(new BasicNameValuePair("huntid", Integer.toString(huntId)));
				parametersForPasswordCheck.add(new BasicNameValuePair("userid", Integer.toString(userId)));
				parametersForPasswordCheck.add(new BasicNameValuePair("huntname", currentHunt));
				parametersForPasswordCheck.add(new BasicNameValuePair("password", password));
				try{
				Log.d("request", "starting");
				JSONObject json = jsonParserForRegister.makeHttpRequest(registerUserWithHuntUrl, "POST", parametersForPasswordCheck);
				Log.d("Register With Hunt Attempt", json.toString());
					
				success = json.getInt(tagSuccess);
				if(success == 1)
				{							
					Log.d("Registration Successful!", json.toString());
					Intent scanQRCodeActivity = new Intent(RegisterWithHuntActivity.this, ScanQRCodeActivity.class);
					finish();
					startActivity(scanQRCodeActivity);
					return json.getString(tagMessage);
				}
				else
				{
					Log.d("Registration failed!", json.getString(tagMessage));
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
				Toast.makeText(RegisterWithHuntActivity.this, fileUrl, Toast.LENGTH_LONG).show();
			} else {
				Toast.makeText(RegisterWithHuntActivity.this, "Nothing returned from the database", Toast.LENGTH_LONG).show();
			}
		}

		@Override
		protected void onCancelled() {
			mAuthTask = null;
		}
	}

}
