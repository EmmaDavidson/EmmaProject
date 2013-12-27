package com.application.treasurehunt.test;

import static org.junit.Assert.*;

import org.junit.Before;
import org.junit.Test;

import com.application.treasurehunt.LoginActivity;
import com.application.treasurehunt.RegisterActivity;

import android.test.ActivityInstrumentationTestCase2;
import android.widget.Button;
import android.widget.TextView;

public class RegisterActivityTest extends ActivityInstrumentationTestCase2<RegisterActivity>{

	private RegisterActivity mRegisterActivity;
	private TextView mNameView;
	private TextView mEmailView;
	private TextView mPasswordView;
	private Button mSaveButton;
	private String mName;
	private String mEmail;
	private String mPassword;
	
	public RegisterActivityTest(Class<RegisterActivity> name) {
		super(name);
	}
	
	public RegisterActivityTest()
	{
		super(RegisterActivity.class);
	}

	@Before
	public void setUp() throws Exception {
		
		mRegisterActivity = getActivity();
		mNameView = (TextView) mRegisterActivity.findViewById(com.application.treasurehunt.R.id.register_name);
		mEmailView = (TextView) mRegisterActivity.findViewById(com.application.treasurehunt.R.id.register_email_address);
		mPasswordView = (TextView) mRegisterActivity.findViewById(com.application.treasurehunt.R.id.register_password);
		mSaveButton = (Button) mRegisterActivity.findViewById(com.application.treasurehunt.R.id.register_save_button);
		
		mRegisterActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mNameView.setText(null);
				mEmailView.setText(null);
				mPasswordView.setText(null);
			};
			
		});
	}
	
	public void testPreconditions()
	{
		mName = mNameView.getText().toString();
		mEmail = mEmailView.getText().toString();
		mPassword = mPasswordView.getText().toString();
			
		assertNotNull("mLoginActivity is null", mRegisterActivity);
		assertEquals("mName is empty","", mName);
		assertEquals("Email is empty", "", mEmail);
		assertEquals("mPassword is empty","", mPassword);
	}
	
	public void nameInvalidIfEmpty()
	{
		mRegisterActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
		mNameView.setText(null);
		assertEquals("Name is empty", false, mRegisterActivity.isValidName());
			}
			
		});
	}
	
	public void nameInvalidIfInvalidLength()
	{
		mRegisterActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mNameView.setText("em");
				assertEquals("Name is invalid length", false, mRegisterActivity.isValidName());
			}
			
		});
			
	}

	public void emailInvalidIfEmpty()
	{
		mRegisterActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mEmailView.setText(null);
				assertEquals("Email is empty", false, mRegisterActivity.isValidEmailAddress());
			}
			
		});
	}
	
	public void emailInvalidIfInvalidLength()
	{
		mRegisterActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mEmailView.setText("emma@com");
				assertEquals("Email is invalid length", false, mRegisterActivity.isValidEmailAddress());
			}
		});
	}
	
	public void emailInvalidIfIncorrectFormat()
	{
		mRegisterActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mEmailView.setText("emmadotcom");
				assertEquals("Email is invalid format", false, mRegisterActivity.isValidEmailAddress());
			}
		});
	}
	
	public void passwordInvalidIfEmpty()
	{
		mRegisterActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mPasswordView.setText(null);
				assertEquals("Password is empty", false, mRegisterActivity.isValidPassword());
			}
		});
			
	}
	
	public void passwordInvalidIfInvalidLength()
	{
		mRegisterActivity.runOnUiThread(new Runnable()
		{
			public void run()
			{
				mPasswordView.setText("em");
				assertEquals("Password is invalid length", false, mRegisterActivity.isValidPassword());
			}
		});
	}

	public void ScanQRCodeActivityBeginsIfSuccessfullyLoggedIn()
	{
		
	}
	
	public void RegisterFailsIfEmailAlreadyInUse()
	{
		
	}
}
