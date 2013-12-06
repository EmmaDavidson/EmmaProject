<?php

	require("config.inc.php");

	try {		
		$query = $db->prepare("SELECT HuntName FROM hunt");
		$result = $query->execute();
	    }

	catch (PDOException $ex) 
	    {
	        
	        $response["success"] = 0;
	        $response["message"] = "Database Error. Please Try Again!";

	        die(json_encode($response));
	    }

	    $users = $query->fetchAll();

            if($users)
	    {
		$response["success"] = 1;
	        $response["message"] = "Sucessfully returned hunts!";
		$response["results"] = $users;

		echo json_encode($response);
	    }		
	    else
	    {
		$response["success"] = 0;
	        $response["message"] = "No hunts were returned.";
	  	echo json_encode($response);
	    }

?>