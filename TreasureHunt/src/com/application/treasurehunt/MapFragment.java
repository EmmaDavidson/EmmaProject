package com.application.treasurehunt;

import java.util.List;

import com.application.treasurehunt.R;
import com.application.treasurehunt.R.id;
import com.application.treasurehunt.R.layout;

import sqlLiteDatabase.Leaderboard;
import sqlLiteDatabase.MapData;
import sqlLiteDatabase.MapDataDAO;
import Utilities.LeaderboardListAdapter;
import Utilities.MapDataListAdapter;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.location.Location;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class MapFragment extends Fragment {

	MapManager mMapManager;
	MapDataDAO mMapDAO;
	private MapData mMapData;
	private Location mLastLocation;
	
	SharedPreferences.Editor editor;
	SharedPreferences settings;
	
	ListView mMapDataListView;
	
	private int currentHuntId;
	
	private BroadcastReceiver mLocationReceiver = new LocationReceiver()
	{
		@Override
		protected void onLocationReceived(Context context, Location loc)
		{
			mLastLocation = loc;
			if(isVisible())
			{
				updateUI();
			}
		}
		
		@Override 
		protected void onProviderEnabledChanged(boolean enabled)
		{	//should be int
			String toastText = enabled ? "enabled" : "disabled";
			// R.string.gps_enabled : R.string.gps_disabled
			Toast.makeText(getActivity(), toastText, Toast.LENGTH_LONG).show();
		}
	};
	
	@Override
	public void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setRetainInstance(true);
		mMapManager = MapManager.get(getActivity());
		
		mMapDAO = new MapDataDAO(getActivity());
		mMapDAO.open();
		
		settings = getActivity().getSharedPreferences("UserPreferencesFile", 0);
		editor = settings.edit();
		
		currentHuntId = settings.getInt("currentHuntId", 0);	
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
	{
		View view = inflater.inflate(R.layout.fragment_map, container, false);
		mMapDataListView = (ListView) view.findViewById(R.id.map_list_view);
		//Here is where all of the text on screen should be set up
		updateUI();
		
		return view;
	}
	
	@Override
	public void onStart()
	{
		super.onStart();
		getActivity().registerReceiver(mLocationReceiver, new IntentFilter(MapManager.ACTION_LOCATION));
	}
	
	@Override
	public void onStop()
	{
		getActivity().unregisterReceiver(mLocationReceiver);
		super.onStop();
	}
	
	private void updateUI()
	{
		boolean started = mMapManager.isTrackingHunt();
		
		//Have to get the proper participant id here
		List<MapData> listOfLeaderboardResults = mMapDAO.getAllMapDataForParticularParticipantId(1, currentHuntId);
		
		//Need to change this back to leaderboard for displaying all of the values... displaying in a table instead!
		//Android book - The Big Nerd Ranch Guide
		MapDataListAdapter adapter = new MapDataListAdapter(getActivity(), listOfLeaderboardResults);
		mMapDataListView.setAdapter(adapter);	
		
		//This has to be passed in from when the map has already been first started
		/*if(mMapData != null)
		{
			//Need to do call of async to get the start time
			if(mMapData != null && mLastLocation != null)
			{
				//Update the data on screen of what you want
				mLatitiudeTextView.setText(Double.toString(mLastLocation.getLatitude()));
				mLongtitudeTextView.setText(Double.toString(mLastLocation.getLongitude()));
				mAltitudeTextView.setText(Double.toString(mLastLocation.getAltitude()));
				//get the duration by calling the async method
				//i.e. need to do the conversion for elapsed time
				//mDurationTextView.setText(MapData.formatDuration(durationSeconds));
			}
		}*/
	}
}
