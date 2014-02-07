package sqlLiteDatabase;

import android.text.format.Time;

public class HuntResults {
	
	 private int huntParticipantId;
	 private Time currentTime;
	 private int tally;

	  public int getHuntParticipantId() {
	    return huntParticipantId;
	  }

	  public Time getCurrentTime() {
	    return currentTime;
	  }
	  
	  public int getTally() {
		    return tally;
		  }
	  

	  public void setHuntParticipantId(int id) {
	    this.huntParticipantId = id;
	  }
	  
	  public void setCurrentTime(Time time) {
		    this.currentTime = time;
		  }
	  
	  public void setTally(int tally) {
		    this.tally = tally;
		  }

}
