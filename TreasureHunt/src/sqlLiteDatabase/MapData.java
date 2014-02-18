package sqlLiteDatabase;

import java.util.Date;


//Originally from Big Nerd Ranch book but adapted for my own use
public class MapData {
	
	private int participantId;
	private int huntId;
	private long mLatitude;
	private long mLongtitude;
	private long mAltitude;
	private long mTimeStamp;
	
	public MapData()
	{
		
	}
	
	public int getHuntId() 
	 {
		 return huntId;
	 }

	 public void setHuntId(int id) 
	 {
		 this.huntId = id;
	 }
	
	 public int getParticipantId() 
	 {
		 return participantId;
	 }

	 public void setParticipantId(int id) 
	 {
		 this.participantId = id;
	 }
	 
	 public long getLatitude() 
	 {
		 return mLatitude;
	 }

	 public void setLatitude(long latitude) 
	 {
		 this.mLatitude = latitude;
	 }
	 
	 public long getLongtitude() 
	 {
		 return mLongtitude;
	 }

	 public void setLongtitude(long longtitude) 
	 {
		 this.mLongtitude = longtitude;
	 }
	 

	 public long getAltitude() 
	 {
		 return mAltitude;
	 }

	 public void setAltitude(long altitude) 
	 {
		 this.mAltitude = altitude;
	 }
	
	 public long getElapsedTime() 
	 {
		 return mTimeStamp;
	 }

	 public void setTimeStamp(long time) 
	 {
		 this.mTimeStamp = time;
	 }
	
	public int getDurationSeconds(long timeTaken)
	{
		//NEED TO WORK OUT THE DURATION
		return 0;
	}
	
	public static String formDuration(int durationSeconds)
	{
		//Work out the complete duration here
		//return the result here
		return String.format("%02d:%02d:%02d", "");
	}

}
