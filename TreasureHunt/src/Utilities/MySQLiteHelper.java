package Utilities;
//http://www.vogella.com/articles/AndroidSQLite/article.html
import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

public class MySQLiteHelper extends SQLiteOpenHelper {

  public static final String TABLE_HUNTS = "Hunt";
  public static final String COLUMN_HUNTNAME = "HuntName";
  
  public static final String TABLE_USER_HUNTS = "UserHunt";
  public static final String COLUMN_USER_HUNTS_HUNTNAME = "HuntName";
  public static final String COLUMN_USER_HUNTS_ID = "HuntId";
  
  public static final String TABLE_HUNT_LEADERBOARD = "Leaderboard";
  public static final String COLUMN_LEADERBOARD_USERNAME = "UserName";
  public static final String COLUMN_LEADERBOARD_TALLY = "Tally";
  public static final String COLUMN_LEADERBOARD_ELAPSED_TIME = "ElapsedTime";
  
  public static final String TABLE_MAPS = "Map";
  public static final String COLUMM_MAPS_PARTICIPANT_ID = "HuntParticipantId";
  public static final String COLUMN_MAPS_HUNT_ID = "HuntId";
  public static final String COLUMN_MAPS_LATITUDE = "Latitude";
  public static final String COLUMN_MAPS_LONGTITUDE = "Longtitude";
  public static final String COLUMN_MAPS_ALTITUDE = "Altitude";
  public static final String COLUMN_MAPS_TIME_STAMP= "TimeStamp";

  private static final String DATABASE_NAME = "TreasureHunt.db";
  private static final int DATABASE_VERSION = 1;

  // Database creation sql statement
  //http://stackoverflow.com/questions/14137622/confused-regarding-sqliteopenhelper-and-creating-multiple-tables
  private static final String DATABASE_CREATE = "create table "
      + TABLE_HUNTS + "("  + COLUMN_HUNTNAME
      + " text not null); " ;
  
  private static final String DATABASE_CREATE_USER_HUNTS =  "create table " 
		  + TABLE_USER_HUNTS + " ("  + COLUMN_USER_HUNTS_ID + " INTEGER NOT NULL, " + COLUMN_USER_HUNTS_HUNTNAME
	      + " text not null); " ;
  
  //http://stackoverflow.com/questions/15424382/sqlite-database-android-create-table
  //http://stackoverflow.com/questions/19619418/android-sqlite-sqliteexception-near-syntax-error
  private static final String DATABASE_CREATE_LEADERBOARD =  "create table " 
		  + TABLE_HUNT_LEADERBOARD + " (" + COLUMN_LEADERBOARD_USERNAME + " name not null, " +
	        COLUMN_LEADERBOARD_TALLY + " INTEGER NOT NULL, " + COLUMN_LEADERBOARD_ELAPSED_TIME + " FLOAT NOT NULL); ";
  
  //MIGHT NEED TO CHANGE START DATE TO DOUBLE
  private static final String DATABASE_CREATE_MAPS = "create table " + TABLE_MAPS + " (" + COLUMM_MAPS_PARTICIPANT_ID + " INTEGER NOT NULL, "
		  									+ COLUMN_MAPS_HUNT_ID + " INTEGER NOT NULL," 
		  									+ COLUMN_MAPS_LATITUDE + " real, " + COLUMN_MAPS_LONGTITUDE + " real, " 
		  									 + COLUMN_MAPS_ALTITUDE + " real, " + COLUMN_MAPS_TIME_STAMP + " INTEGER NOT NULL); " ;

  public MySQLiteHelper(Context context) {
    super(context, DATABASE_NAME, null, DATABASE_VERSION);
  }

  @Override
  public void onCreate(SQLiteDatabase database) {
    database.execSQL(DATABASE_CREATE);
    database.execSQL(DATABASE_CREATE_USER_HUNTS);
    database.execSQL(DATABASE_CREATE_LEADERBOARD);
    database.execSQL(DATABASE_CREATE_MAPS);
  }

  //WE REALLY WANT A HELPER FOR EACH DIFFERENT TABLE - UPDATE LATER 
  @Override
  public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
    Log.w(MySQLiteHelper.class.getName(),
        "Upgrading database from version " + oldVersion + " to "
            + newVersion + ", which will destroy all old data");
    db.execSQL("DROP TABLE IF EXISTS " + TABLE_HUNTS);
    db.execSQL("DROP TABLE IF EXISTS " + TABLE_USER_HUNTS);
    db.execSQL("DROP TABLE IF EXISTS " + TABLE_HUNT_LEADERBOARD);
    db.execSQL("DROP TABLE IF EXISTS " + TABLE_MAPS);
    onCreate(db);
  }
  

} 
