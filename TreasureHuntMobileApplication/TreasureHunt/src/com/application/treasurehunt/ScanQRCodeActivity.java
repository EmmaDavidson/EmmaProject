package com.application.treasurehunt;

import com.dm.zbar.android.scanner.ZBarConstants;
import com.dm.zbar.android.scanner.ZBarScannerActivity;
//import com.google.zxing.integration.android.IntentIntegrator;
//import com.google.zxing.integration.android.IntentResult;

import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

//http://mobile.tutsplus.com/tutorials/android/android-sdk-create-a-barcode-reader/ - ZXING
//https://github.com/DushyanthMaguluru/ZBarScanner for ZBAR - USING THIS CURRENTLY, referencing the source forge project
//Whole Activity taken from this either of these websites

public class ScanQRCodeActivity extends Activity implements OnClickListener {

	private Button scanButton;
	private TextView contentText;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_scan_qrcode);
		
		scanButton = (Button)findViewById(R.id.scan_qr_code_button);
		contentText = (TextView)findViewById(R.id.scan_content_received);
		scanButton.setOnClickListener(this);
	}
	
	//For now - deciding to use ZBar within application so user does not have to have another application running to scan
	@Override
	public void onClick(View v) {	
		Intent intent = new Intent(this, ZBarScannerActivity.class);
		startActivityForResult(intent, 1);
	}
	
	public void onActivityResult(int requestCode, int resultCode, Intent intent) 
	{
		if (resultCode == RESULT_OK) 
	    {	
			contentText.setText("QUESTION: " + intent.getStringExtra(ZBarConstants.SCAN_RESULT));

	    } else if(resultCode == RESULT_CANCELED) {
	        Toast.makeText(this, "Camera unavailable", Toast.LENGTH_SHORT).show();
	    }
	}

}
