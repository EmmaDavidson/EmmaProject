package com.application.treasurehunt;

import java.util.List;

import sqlLiteDatabase.MapData;
import sqlLiteDatabase.MapDataDAO;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.location.Location;
import android.location.LocationManager;
import android.util.Log;

//Complete class from tutorial by Big Nerd Ranch
public class MapManager {
	
	private static final String TAG = "MapManager";
	public static final String ACTION_LOCATION = "com.application.treasurehunt.ACTION_LOCATION";
	
	private static MapManager mMapManager;
	private Context mAppContext;
	private LocationManager mLocationManager;
	
	private MapDataDAO mMapDAO;
	private int currentHuntParticipantId;
	private int currentHuntId;
	
	SharedPreferences.Editor editor;
	SharedPreferences settings;
	
	private MapManager(Context appContext)
	{
		mAppContext = appContext;
		mLocationManager = (LocationManager) mAppContext.getSystemService(Context.LOCATION_SERVICE);
		
		mMapDAO = new MapDataDAO(mAppContext);
		mMapDAO.open();
		
		settings = mAppContext.getSharedPreferences("UserPreferencesFile", 0);
		editor = settings.edit();
		
		currentHuntParticipantId = settings.getInt("CurrentMapParticipantId", 0);
		currentHuntId = settings.getInt("currentHuntId", 0);
	}
	
	public static MapManager get(Context c)
	{
		if(mMapManager == null)
		{
			mMapManager = new MapManager(c.getApplicationContext());
		}
		return mMapManager;
	}
	
	private PendingIntent getLocationPendingIntent(boolean shouldCreate)
	{
		Intent broadcast = new Intent(ACTION_LOCATION);
		int flags = shouldCreate ? 0 : PendingIntent.FLAG_NO_CREATE;
		return PendingIntent.getBroadcast(mAppContext, 0, broadcast, flags);
	}
	
	public void startLocationUpdates()
	{
		String provider = LocationManager.GPS_PROVIDER;
		
		Location lastKnown = mLocationManager.getLastKnownLocation(provider);
		if(lastKnown != null)
		{
			lastKnown.setTime(System.currentTimeMillis());
			broadcastLocation(lastKnown);
		}
		
		PendingIntent pi = getLocationPendingIntent(true);
		mLocationManager.requestLocationUpdates(provider, 0, 0, pi);
	}
	
	private void broadcastLocation(Location location)
	{
		Intent broadcast = new Intent(ACTION_LOCATION);
		broadcast.putExtra(LocationManager.KEY_LOCATION_CHANGED, location);
		mAppContext.sendBroadcast(broadcast);
	}
	
	public void stopLocationUpdates()
	{
		PendingIntent pi = getLocationPendingIntent(false);
		if(pi != null)
		{
			mLocationManager.removeUpdates(pi);
			pi.cancel();
		}
	}
	
	public boolean isTrackingHunt()
	{
		return getLocationPendingIntent(false) != null;
	}
	
	public void startTrackingMap(MapData map)
	{
		currentHuntParticipantId = map.getParticipantId();
		editor.putInt("CurrentMapParticipantId", currentHuntParticipantId);
		editor.commit();
		startLocationUpdates();
	}
	
	public void stopMap()
	{
		stopLocationUpdates();
		currentHuntParticipantId = -1;
		editor.remove("CurrentMapParticipantId");
		editor.commit();
	}
	
	public void insertLocation(Location loc)
	{
		if(currentHuntParticipantId != -1)
		{
			mMapDAO.insertLocation(currentHuntParticipantId, loc, currentHuntId);
		}
		else
		{
			Log.e("Map", "Location received with no tracking map");
		}
	}
	
	public List<MapData> queryMaps()
	{
		return mMapDAO.getAllMapDataForParticularParticipantId(currentHuntParticipantId, currentHuntId);
	}
	
	private MapData insertMap(int participantId)
	{
		MapData map = mMapDAO.insertMapData(participantId, currentHuntId);
		return map;
	}
	
	

}
