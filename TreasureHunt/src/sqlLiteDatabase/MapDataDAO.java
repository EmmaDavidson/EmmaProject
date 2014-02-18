package sqlLiteDatabase;

import java.util.ArrayList;
import java.util.List;

import Utilities.MySQLiteHelper;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.location.Location;

public class MapDataDAO {

	private SQLiteDatabase database;
	  private MySQLiteHelper dbHelper;
	  private String[] allColumns = { MySQLiteHelper.COLUMM_MAPS_PARTICIPANT_ID, MySQLiteHelper.COLUMN_MAPS_HUNT_ID, MySQLiteHelper.COLUMN_MAPS_LATITUDE, MySQLiteHelper.COLUMN_MAPS_LONGTITUDE, MySQLiteHelper.COLUMN_MAPS_ALTITUDE , MySQLiteHelper.COLUMN_MAPS_TIME_STAMP };
	  
	  public MapDataDAO(Context context) {
	    dbHelper = new MySQLiteHelper(context);
	  }

	  public void open() throws SQLException {
		  database = dbHelper.getWritableDatabase();
		  //Unclean - drops the database each time and re-adds the table.
		  updateDatabaseLocally();
	  }
	  
	  public void updateDatabaseLocally()
	  {
		  dbHelper.onUpgrade(database, database.getVersion(), database.getVersion());
	  }

	  public void close() {
	    dbHelper.close();
	  }
	  
	  public MapData insertMapData(int participantId, int huntId)
	  {
		  ContentValues values = new ContentValues();
		  values.put(MySQLiteHelper.COLUMM_MAPS_PARTICIPANT_ID, participantId);
		  values.put(MySQLiteHelper.COLUMN_MAPS_HUNT_ID, huntId);
		  values.put(MySQLiteHelper.COLUMN_MAPS_LATITUDE, 0);
		  values.put(MySQLiteHelper.COLUMN_MAPS_LONGTITUDE, 0);
		  values.put(MySQLiteHelper.COLUMN_MAPS_ALTITUDE, 0);
		  values.put(MySQLiteHelper.COLUMN_MAPS_TIME_STAMP, 0);
		  
		  long insertId = database.insert(MySQLiteHelper.TABLE_MAPS, null, values);
		  
		  Cursor cursor = database.query(MySQLiteHelper.TABLE_MAPS,
			        allColumns, null, null,
			        null, null, null);
			    cursor.moveToFirst();
			    MapData newMapData = cursorToMapEntry(cursor);
			    cursor.close();
			    return newMapData;
	  }
	  
	  //Needs to be normalised more - need to create a separate table for adding the start date so not repetitive
	  public MapData insertLocation(int participantId, Location location, int huntId)
	  {
		  ContentValues values = new ContentValues();
		  values.put(MySQLiteHelper.COLUMM_MAPS_PARTICIPANT_ID, participantId);
		  values.put(MySQLiteHelper.COLUMN_MAPS_HUNT_ID, huntId);
		  values.put(MySQLiteHelper.COLUMN_MAPS_LATITUDE, location.getLatitude());
		  values.put(MySQLiteHelper.COLUMN_MAPS_LONGTITUDE, location.getLongitude());
		  values.put(MySQLiteHelper.COLUMN_MAPS_ALTITUDE, location.getAltitude());
		  values.put(MySQLiteHelper.COLUMN_MAPS_TIME_STAMP, location.getTime());
		  
		  long insertId =  database.insert(MySQLiteHelper.TABLE_MAPS, null, values);
		  
		  Cursor cursor = database.query(MySQLiteHelper.TABLE_MAPS,
			        allColumns, null, null,
			        null, null, null);
			    cursor.moveToFirst();
			    MapData newMapData = cursorToMapEntry(cursor);
			    cursor.close();
			    return newMapData;
	  }
	  
	  public List<MapData> getAllMapDataForParticularParticipantId(int participantId, int huntId) {
		    List<MapData> mapDataEntries = new ArrayList<MapData>();
		    //http://stackoverflow.com/questions/12339121/multiple-orderby-in-sqlitedatabase-query-method
		    String orderBy = MySQLiteHelper.COLUMN_MAPS_TIME_STAMP + " DESC";
		    String arguments = "HuntParticipantId= " + participantId + ", HuntId=" + huntId;
		    Cursor cursor = database.query(MySQLiteHelper.TABLE_MAPS,
		    		allColumns, arguments, null, null, null, orderBy);

		    cursor.moveToFirst();
		    while (!cursor.isAfterLast()) {
		      MapData mapEntry = cursorToMapEntry(cursor);
		      mapDataEntries.add(mapEntry);
		     
		      cursor.moveToNext();
		    } 
		      cursor.close();
			    return mapDataEntries;
		    }
	  
	  //http://stackoverflow.com/questions/6781954/android-3-0-couldnt-read-row-column-from-cursor-window
	  private MapData cursorToMapEntry(Cursor cursor) {
		  MapData mapDataResult = new MapData();
		  	mapDataResult.setParticipantId(cursor.getInt(0));
		  	mapDataResult.setHuntId(cursor.getInt(1));
		    mapDataResult.setLatitude(cursor.getLong(2));
		    mapDataResult.setLongtitude(cursor.getLong(3));
		    mapDataResult.setAltitude(cursor.getLong(4));
		    mapDataResult.setTimeStamp(cursor.getLong(5));
		    
		    return mapDataResult;
	  }
	  
}
