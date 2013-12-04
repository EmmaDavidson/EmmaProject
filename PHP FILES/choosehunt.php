//http://www.php.net/manual/en/pdostatement.fetchall.php
//http://stackoverflow.com/questions/14491430/using-pdo-to-echo-display-all-rows-from-a-table
<?php

	require("config.inc.php");


	try {		
		$query = $db->prepare("SELECT * FROM hunt");
		$result = $query->execute();
	    }
	    catch (PDOException $ex) 
	    {
	        
	        $response["success"] = 0;
	        $response["message"] = "Database Error. Please Try Again!";
	        die(json_encode($response));
	 }
			$response["success"] = 1;
	        	$response["message"] = "Sucessfully returned hunts!";
	
			//echo json_encode($response);
			$users = $query->fetchAll();
					

			foreach($users as $user):  
    			echo($user['HuntName']);  
			endforeach;
?>