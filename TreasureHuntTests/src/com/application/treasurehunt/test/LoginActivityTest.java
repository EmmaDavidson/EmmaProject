package com.application.treasurehunt.test;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.Matchers;
import org.mockito.runners.*;
import org.robolectric.Robolectric;

import Utilities.JSONParser;
import android.R;
import android.app.Instrumentation.ActivityMonitor;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Handler;
import android.test.ActivityInstrumentationTestCase2;
import android.util.Log;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.application.treasurehunt.ChooseHuntActivity;
import com.application.treasurehunt.LoginActivity;
import com.application.treasurehunt.LoginActivity.UserLoginTask;
import com.application.treasurehunt.RegisterActivity;
import com.application.treasurehunt.R.id;
//http://developer.android.com/training/activity-testing/activity-basic-testing.html

public class LoginActivityTest extends ActivityInstrumentationTestCase2<LoginActivity> {

	private LoginActivity mLoginActivity;
	private TextView mEmailView;
	private TextView mPasswordView;
	private Button mRegisterButton;
	private Button mLoginButton;
	private String mEmail;
	private String mPassword;
	@Mock private JSONParser jsonParserMock;
	
	//http://stackoverflow.com/questions/9053864/no-enclosing-instance-of-type-error-while-calling-method-from-another-class-in
		
	private LoginActivity.UserLoginTask loginTask;
	
	public LoginActivityTest(Class<LoginActivity> name) {
		super(name);
	}
	
	public LoginActivityTest()
	{
		super(LoginActivity.class);
	}

	@Override
	protected void setUp() throws Exception {
		super.setUp();
		mLoginActivity = getActivity();
		mEmailView = (TextView) mLoginActivity.findViewById(com.application.treasurehunt.R.id.login_email_address);
		mPasswordView = (TextView) mLoginActivity.findViewById(com.application.treasurehunt.R.id.login_password);
		mRegisterButton = (Button) mLoginActivity.findViewById(com.application.treasurehunt.R.id.register_on_login_button);
		mLoginButton = (Button) mLoginActivity.findViewById(com.application.treasurehunt.R.id.sign_in_button);
		
		loginTask = mLoginActivity.new UserLoginTask();
		jsonParserMock = Mockito.mock(JSONParser.class);
		
		//http://stackoverflow.com/questions/9694282/activityunittestcase-and-activityrunonuithread
		mLoginActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mEmailView.setText(null);
				mPasswordView.setText(null);
			};
			
		});
	}
	
	public void testPreconditions()
	{
		mEmail = mEmailView.getText().toString();
		mPassword = mPasswordView.getText().toString();
		
		assertNotNull("mLoginActivity is null", mLoginActivity);
		assertEquals("mPassword is empty","", mPassword);
		assertEquals("Email is empty", "", mEmail);
	}
	
	public void emailInvalidIfEmpty()
	{
		mLoginActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mEmailView.setText(null);
				assertEquals("Email is empty", false, mLoginActivity.isValidEmailAddress());
			}
		});
	}
	
	public void emailInvalidIfInvalidLength()
	{
		mLoginActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mEmailView.setText("emma@com");
				assertEquals("Email is invalid length", false, mLoginActivity.isValidEmailAddress());
			}
		});
		
	}
	
	public void emailInvalidIfIncorrectFormat()
	{
		mLoginActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mEmailView.setText("emmadotcom");
				assertEquals("Email is invalid format", false, mLoginActivity.isValidEmailAddress());
			}
			
		});
	}
	
	public void passwordInvalidIfEmpty()
	{
		mLoginActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mPasswordView.setText(null);
				assertEquals("Password is empty", false, mLoginActivity.isValidPassword());
			}
		});
	}
	
	public void passwordInvalidIfInvalidLength()
	{
		mLoginActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mPasswordView.setText("em");
				assertEquals("Password is invalid length", false, mLoginActivity.isValidPassword());
			}
		});
			
	}
	
	//http://stackoverflow.com/questions/9405561/test-if-a-button-starts-a-new-activity-in-android-junit-pref-without-robotium
	public void registerActivityShouldStartIfRegisterButtonPressed()
	{
		ActivityMonitor activityMonitor = getInstrumentation().addMonitor(RegisterActivity.class.getName(), null, false);
		
		mLoginActivity.runOnUiThread(new Runnable(){
			@Override
			public void run() {
				mRegisterButton.performClick();
			}
			
		});
		
		RegisterActivity nextActivity = (RegisterActivity) getInstrumentation().waitForMonitorWithTimeout(activityMonitor, 5);
		assertNotNull(nextActivity);
		nextActivity.finish();
	}
	
	public void chooseHuntActivityBeginsIfSuccessfullyLoggedIn() throws JSONException
	{	
		/*Object jsonMessage = "message" + ":" + "Successfully logged in!" + "," + "success:1";
		jsonParserMock = Mockito.mock(JSONParser.class);
	
		//Mockito.when(jsonParserMock.makeHttpRequest(Matchers.anyString(), Matchers.anyString(), Matchers.anyList())).thenReturn(fakeObject);
	
		//http://stackoverflow.com/questions/9694282/activityunittestcase-and-activityrunonuithread
		mLoginActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mEmailView.setText("aewart@gmail.com");
				mPasswordView.setText("password");
			};
			
		});	
		
		*/
	}
	
	public void loginFailsIfIncorrectDetails()
	{
		final UserLoginTask jsonMock = Mockito.mock(UserLoginTask.class);		
		
		JSONParser jsonParserMock = Mockito.mock(JSONParser.class);	
		JSONObject fakeJsonObject = null;
		try {
			fakeJsonObject = new JSONObject("message" + ":" + "Login details incorrect." + "," + "success:0");
		} catch (JSONException e1) {
			e1.printStackTrace();
		}
		
		Mockito.when(jsonParserMock.makeHttpRequest(Matchers.anyString(), Matchers.anyString(), Matchers.anyList())).thenReturn(fakeJsonObject);
		
		//http://stackoverflow.com/questions/9694282/activityunittestcase-and-activityrunonuithread
		mLoginActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mEmailView.setText("aewart@gmail.com");
				mPasswordView.setText("ujhdsfksdf");
			};
			
		});
		
		try {
			runTestOnUiThread(new Runnable(){
				 
				public void run()
				{
					loginTask.doInBackground(Matchers.anyString());
				}
				
			});
		} catch (Throwable e) 
		{
			e.printStackTrace();
		}
			
		//Assert that the call was made
		Mockito.verify(jsonParserMock, Mockito.times(1)).makeHttpRequest(Matchers.anyString(), Matchers.anyString(), Matchers.anyList());
	
		//Assert that the choose hunt activity is started next
		ActivityMonitor activityMonitor = getInstrumentation().addMonitor(ChooseHuntActivity.class.getName(), null, false);
		ChooseHuntActivity nextActivity = (ChooseHuntActivity) getInstrumentation().waitForMonitorWithTimeout(activityMonitor, 5);
		assertNull(nextActivity);
		nextActivity.finish();
	}
	
	public void loginFailsIfUserDoesNotExist()
	{
		final UserLoginTask jsonMock = Mockito.mock(UserLoginTask.class);		
		
		JSONParser jsonParserMock = Mockito.mock(JSONParser.class);	
		JSONObject fakeJsonObject = null;
		try {
			fakeJsonObject = new JSONObject("message" + ":" + "User does not exist." + "," + "success:0");
		} catch (JSONException e1) {
			e1.printStackTrace();
		}
		
		Mockito.when(jsonParserMock.makeHttpRequest(Matchers.anyString(), Matchers.anyString(), Matchers.anyList())).thenReturn(fakeJsonObject);
		
		//http://stackoverflow.com/questions/9694282/activityunittestcase-and-activityrunonuithread
		mLoginActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mEmailView.setText("aiujdfioudigt@gmail.com");
				mPasswordView.setText("ujhdsfksdf");
			};
			
		});
		
		try {
			runTestOnUiThread(new Runnable(){
				 
				public void run()
				{
					//jsonMock.execute((String) null);
					jsonMock.doInBackground((String)null);
				}
				
			});
		} catch (Throwable e) 
		{
			e.printStackTrace();
		}
			
		//Assert that the call was made
		Mockito.verify(jsonParserMock, Mockito.times(1)).makeHttpRequest(Matchers.anyString(), Matchers.anyString(), Matchers.anyList());
		
		//Assert that the choose hunt activity is started next
		ActivityMonitor activityMonitor = getInstrumentation().addMonitor(ChooseHuntActivity.class.getName(), null, false);
		ChooseHuntActivity nextActivity = (ChooseHuntActivity) getInstrumentation().waitForMonitorWithTimeout(activityMonitor, 5);
		assertNull(nextActivity);
		nextActivity.finish();
	}
}
