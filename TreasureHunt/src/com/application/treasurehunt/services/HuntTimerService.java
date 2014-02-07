package com.application.treasurehunt.services;

import java.text.SimpleDateFormat;
import java.util.Date;

import android.app.Service;
import android.content.Intent;
import android.os.Binder;
import android.os.IBinder;
import android.text.format.Time;
import android.widget.Toast;

//http://www.youtube.com/watch?v=or3boiYi-lI
public class HuntTimerService extends Service {

	private IBinder mBinder = new LocalBinder();
	SimpleDateFormat dateFormat;
	String startDateTime;
	
	@Override
	public IBinder onBind(Intent arg0) {
		return mBinder;
	}
	
	public void startTimer()
	{	//Want to calculate the date AND time 
		//Hunt may last over several days
		//Want correct calculation
		//http://www.programmingrelief.com/496760/Android-Chronometer-As-A-Persistent-Stopwatch-How-To-Set-Starting-Time%3F-What-Is-Chronometer-base%3F
		dateFormat = new SimpleDateFormat("ddMMyyyy_HHmmss");
		startDateTime = dateFormat.format(new Date());
		
	}
	
	//http://www.vogella.com/tutorials/AndroidServices/article.html
	@Override
	public int onStartCommand(Intent intent, int flags, int startId)
	{
		super.onStartCommand(intent, flags, startId);
		Toast.makeText(this, "Timer Started", Toast.LENGTH_SHORT);
		return Service.START_NOT_STICKY;
	}
	
	//http://www.youtube.com/watch?v=or3boiYi-lI
	public class LocalBinder extends Binder {
	
		public HuntTimerService getService()
		{
			return HuntTimerService.this;
		}
		
		//return the current time taken
		public Time getCurrentHuntTime()
		{
			return null;
		}
	}
	
}



