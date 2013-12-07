package sqlLiteDatabase;

public class Hunt {
	  private long huntId;
	  private String huntName;

	  public long getHuntId() {
	    return huntId;
	  }

	  public void setHuntId(long id) {
	    this.huntId = id;
	  }

	  public String getHuntName() {
	    return huntName;
	  }

	  public void setHuntName(String hunt) {
	    this.huntName = hunt;
	  }

	  // Will be used by the ArrayAdapter in the ListView
	  @Override
	  public String toString() {
	    return huntName;
	  }
	} 