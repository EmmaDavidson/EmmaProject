package com.application.treasurehunt;

import Utilities.SingleFragmentActivity;
import android.os.Bundle;
import android.app.Activity;
import android.support.v4.app.Fragment;
import android.view.Menu;

public class MapActivity extends SingleFragmentActivity {

	@Override
	protected Fragment createFragment()
	{
		
		int participantId = getIntent().getIntExtra("userParticipantIdForMap", -1);
		if(participantId != -1)
		{
			return MapFragment.newInstance(participantId);
		}
		else
		{
			return new MapFragment();
		}
	}

}
