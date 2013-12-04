package com.application.treasurehunt;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;

import com.application.treasurehunt.LoginActivity.UserLoginTask;

import Utilities.JSONParser;
import android.os.AsyncTask;
import android.os.Bundle;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.text.TextUtils;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class RegisterActivity extends Activity {

	private static final String myRegisterUrl =  "http://192.168.1.74:80/webservice/register.php";
	private static final String tagSuccess = "success";
	private static final String tagMessage = "message";
	
	JSONParser jsonParser = new JSONParser();
	
	private UserRegisterTask mAuthTask = null;
	private ProgressDialog pDialog; 
	
	private String mEmail;
	private String mPassword;
	private String mName;

	private EditText mEmailView;
	private EditText mPasswordView;
	private EditText mNameView;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_register);
		mEmailView = (EditText) findViewById(R.id.register_email_address);
		mPasswordView = (EditText) findViewById(R.id.register_password);
		mNameView = (EditText) findViewById(R.id.register_name);
							
		findViewById(R.id.register_save_button).setOnClickListener(
				new View.OnClickListener() {
					@Override
					public void onClick(View view) {
						attemptRegister();
					}
				});
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.register, menu);
		return true;
	}
	
	public void attemptRegister() {
		if (mAuthTask != null) {
			return;
		} 		
		
		mEmailView.setError(null);
		mPasswordView.setError(null);
		mNameView.setError(null);

		mEmail = mEmailView.getText().toString();
		mPassword = mPasswordView.getText().toString();
		mName = mNameView.getText().toString();
		
		if((isValidEmailAddress() && isValidPassword() && isValidName()))
		{		
			mAuthTask = new UserRegisterTask(); // Do ASYNC way
			mAuthTask.execute((String) null);
		}
		
	}
	
	public boolean isValidEmailAddress()
	{	
		if (TextUtils.isEmpty(mEmail)) {
			mEmailView.setError(getString(R.string.error_email_null));
			return false;
			
		}
		else if(mEmail.length() < 10)
		{
			mEmailView.setError(getString(R.string.error_email_too_short));	
			return false;
		}
		
		return true;
		
	}
	
	public boolean isValidPassword()
	{
		if (TextUtils.isEmpty(mPassword)) {
			mPasswordView.setError(getString(R.string.error_password_null));
			return false;
			
		}
		else if(mPassword.length() < 5)
		{
			mPasswordView.setError(getString(R.string.error_password_too_short));	
			return false;
		}
		
		return true;
		
	}
	
	public boolean isValidName()
	{
		if (TextUtils.isEmpty(mName)) {
			mNameView.setError(getString(R.string.error_name_null));
			return false;
			
		}
		else if(mPassword.length() < 5)
		{
			mNameView.setError(getString(R.string.error_name_too_short));	
			return false;
		}
		
		return true;
		
	}
	
		public class UserRegisterTask extends AsyncTask<String, String, String> {
		
		@Override
		protected void onPreExecute()
		{
			super.onPreExecute();
			pDialog = new ProgressDialog(RegisterActivity.this);
            pDialog.setMessage("Attempting register...");
			pDialog.setIndeterminate(false);
			pDialog.setCancelable(true);
			pDialog.show();
		}
		
		@Override
		protected String doInBackground(String... args) {
//http://www.mybringback.com/tutorial-series/13193/android-mysql-php-json-part-5-developing-the-android-application/
			
			int success;
			String email = mEmail;
			String password = mPassword;
			String name = mName;
			
			try {
				List<NameValuePair> parameters = new ArrayList<NameValuePair>();
				
				parameters.add(new BasicNameValuePair("email", email));
				parameters.add(new BasicNameValuePair("password", password));
				parameters.add(new BasicNameValuePair("name", name));
				
				Log.d("request", "starting");
				JSONObject json = jsonParser.makeHttpRequest(myRegisterUrl, "POST", parameters);
				Log.d("Login attempt", json.toString());
				
				success = json.getInt(tagSuccess);
				if(success == 1)
				{
					Log.d("Login Successful!", json.toString());
					Intent loginActivityIntent = new Intent(RegisterActivity.this, LoginActivity.class);
					finish();
					startActivity(loginActivityIntent);
					return json.getString(tagMessage);
				}
				else
				{
					Log.d("Login failed!", json.getString(tagMessage));
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
				Toast.makeText(RegisterActivity.this, fileUrl, Toast.LENGTH_LONG).show();
			} else {
				Toast.makeText(RegisterActivity.this, "Nothing returned from the database", Toast.LENGTH_LONG).show();
			}
		}

		@Override
		protected void onCancelled() {
			mAuthTask = null;
		}
	}

}
