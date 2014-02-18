package Utilities;

import java.util.List;

import sqlLiteDatabase.Leaderboard;
import sqlLiteDatabase.MapData;
import android.app.Activity;
import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.application.treasurehunt.LeaderboardActivity;
import com.application.treasurehunt.MapFragment;
import com.application.treasurehunt.R;

//The big nerd ranch guide
//Also http://www.ezzylearning.com/tutorial.aspx?tid=1763429
	public class MapDataListAdapter extends ArrayAdapter<MapData>
	{
		private Context context;
		private List<MapData> listOfLeaderboardResults;
		
		TextView mLatitudeText;
		TextView mLongtitudeText;
		TextView mAltitudeText;
		
		List<MapData> listOfMapResults;
		
		public MapDataListAdapter(Context mapFragment, List<MapData> mapResults)
		{
			super(mapFragment, 0, mapResults);
			//context = mapFragment;
			listOfMapResults = mapResults;
		}
		
		@Override
		public View getView(int position, View convertView, ViewGroup parent)
		{
			if(convertView == null)
			{
				convertView = ((Activity) context).getLayoutInflater().inflate(R.layout.map_data_list_item, null);
			}
			
			MapData mapDataResult = getItem(position);
			
			mLatitudeText = (TextView) convertView.findViewById(R.id.map_data_latitude);
			mLatitudeText.setText(mapDataResult.getLatitude()+"");
			
			mLongtitudeText = (TextView) convertView.findViewById(R.id.map_data_longtitude);
			mLongtitudeText.setText(mapDataResult.getLongtitude()+"");
			
			mAltitudeText = (TextView) convertView.findViewById(R.id.map_data_altitude);
			mAltitudeText.setText(mapDataResult.getAltitude()+"");
			
			return convertView;
		}
	}
