package com.application.treasurehunt;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;

import Utilities.JSONParser;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

/**
 *FILE GENERATED AUTOMATICALLY WHEN CREATING NEW LOGIN ACTIVITY, although altered to suit my needs
 */
public class LoginActivity extends Activity {
	//http://www.mybringback.com/tutorial-series/13193/android-mysql-php-json-part-5-developing-the-android-application/
	JSONParser jsonParser = new JSONParser();
	//http://stackoverflow.com/questions/5806220/how-to-connect-to-my-http-localhost-web-server-from-android-emulator-in-eclips
	private static final String myLoginUrl =  "http://192.168.1.74:80/webservice/login.php";
	//private static final String myLoginUrl =  "http://143.117.224.68:80/webservice/login.php";
	private static final String tagSuccess = "success";
	private static final String tagMessage = "message";
	
	private UserLoginTask mAuthTask = null;
	private ProgressDialog pDialog; //spinner thing
	
	private String mEmail;
	private String mPassword;

	private EditText mEmailView;
	private EditText mPasswordView;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		setContentView(R.layout.activity_login);
		mEmailView = (EditText) findViewById(R.id.login_email_address);
		mPasswordView = (EditText) findViewById(R.id.login_password);

		findViewById(R.id.sign_in_button).setOnClickListener(
				new View.OnClickListener() {
					@Override
					public void onClick(View view) {
						attemptLogin();
					}
				});
		
		findViewById(R.id.register_on_login_button).setOnClickListener(
				new View.OnClickListener() {
					@Override
					public void onClick(View view) {
						goToRegister();
					}
				});
	}

	/*@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		super.onCreateOptionsMenu(menu);
		getMenuInflater().inflate(R.menu.login, menu);
		return true;
	} */

	public void goToRegister()
	{		
		Intent intent = new Intent(this, RegisterActivity.class);
		startActivity(intent);	
		
		mEmailView.setText(null);
		mPasswordView.setText(null);
	}
	
	public void attemptLogin() {
		if (mAuthTask != null) {
			return;
		} 		
		
		mEmailView.setError(null);
		mPasswordView.setError(null);

		mEmail = mEmailView.getText().toString();
		mPassword = mPasswordView.getText().toString();
		
		if((isValidEmailAddress() && isValidPassword()))
		{		
			mAuthTask = new UserLoginTask(); // Do ASYNC way
			mAuthTask.execute((String) null);
			
			mEmailView.setText(null);
			mPasswordView.setText(null);
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
		//http://stackoverflow.com/questions/1819142/how-should-i-validate-an-e-mail-address-on-android
		else if(!android.util.Patterns.EMAIL_ADDRESS.matcher(mEmail.toString()).matches())
		{
			mEmailView.setError(getString(R.string.error_email_incorrect_format));	
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
		else if(mPassword.length() < 6)
		{
			mPasswordView.setError(getString(R.string.error_password_too_short));	
			return false;
		}
		
		return true;
		
	}

	public class UserLoginTask extends AsyncTask<String, String, String> {
		
		@Override
		protected void onPreExecute()
		{
			super.onPreExecute();
			pDialog = new ProgressDialog(LoginActivity.this);
            pDialog.setMessage("Attempting login...");
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
			
			try {
				List<NameValuePair> parameters = new ArrayList<NameValuePair>();
				
				parameters.add(new BasicNameValuePair("email", email));
				parameters.add(new BasicNameValuePair("password", password));
				
				Log.d("request", "starting");
				JSONObject json = jsonParser.makeHttpRequest(myLoginUrl, "POST", parameters);
				Log.d("Login attempt", json.toString());
				
				success = json.getInt(tagSuccess);
				if(success == 1)
				{
					Log.d("Login Successful!", json.toString());
					Intent scanQRCodeActivityIntent = new Intent(LoginActivity.this, ChooseHuntActivity.class);
					finish();
					startActivity(scanQRCodeActivityIntent);
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
				Toast.makeText(LoginActivity.this, fileUrl, Toast.LENGTH_LONG).show();
			} else {
				Toast.makeText(LoginActivity.this, "Nothing returned from the database", Toast.LENGTH_LONG).show();
			}
		}

		@Override
		protected void onCancelled() {
			mAuthTask = null;
		}
	}
}
