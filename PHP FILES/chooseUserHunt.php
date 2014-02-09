<?php

	require("config.inc.php");

	try {	
		$query = "SELECT HuntId FROM huntparticipants WHERE UserId = 25 ";
		$query_params = array(
	        ':UserId' => $_POST['userId']
	   	 );
	
		$stmt   = $db->prepare($query);
	        $result = $stmt->execute($query_params);
	    }

	catch (PDOException $ex) 
	    {
	        
	        $response["success"] = 0;
	        $response["message"] = "Database Error. Please Try Again!";

	        die(json_encode($response));
	    }

	    $hunts = $stmt->fetchAll();

            if($hunts)
	    {
		$response["results"] = array();
		foreach($hunts as $huntIdReturned)
		{
			try {	
				$huntIdReturned = array_shift($huntIdReturned);
				$query2 = "SELECT HuntName FROM hunt WHERE HuntId = :HuntId ";
				$query_params2 = array(
	    			    ':HuntId' => $huntIdReturned
	   			 );			
				$stmt2   = $db->prepare($query2);
				$result2 = $stmt2->execute($query_params2);
				
	 	        }

			catch (PDOException $ex) 
		    	{
	        
	       	        	$response["success"] = 0;
	        		$response["message"] = "Database Error. Please Try Again!";

		        	die(json_encode($response));
	    		}

	    		$huntName = $stmt2->fetchAll(PDO::FETCH_ASSOC);
			
			if($huntName)
			{
				$response["success"] = 1;
	        		$response["message"] = "Sucessfully returned hunts!";
				array_push($response["results"], $huntName);	
			}
	   	 }
		echo json_encode($response);
		 		
	    }		
	    else
	    {
		$response["success"] = 0;
	        $response["message"] = "No hunts were returned.";
	  	echo json_encode($response);
	    }

?>