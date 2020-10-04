
/****************************************************
------------------ GLOBAL ACCESS VARIABLES ----------
****************************************************/
var parkingData;
/************************************************** */

$(() => {

    let i = 0;
    $("#park").click(() => {
        i = 1,
            $("#park").css("background-color", "lightgreen"),
            $("#unpark").css("background-color", "white"),
            $("#submit").text("Park")
    });
    $("#unpark").click(() => {
        i = 2,
            $("#unpark").css("background-color", "lightgreen"),
            $("#park").css("background-color", "white"),
            $("#submit").text("Unpark")
    });

    $("#submit").click(() => {
        if (i === 1) {           
            park(parkingData);

        }
        if (i === 2) {
            unpark(parkingData);
        }

    });

    $("#validate").click(() => {
        checkPerson(i);
    });
    
});

function park(swapiActorToPark) {
    console.log("Inside park function: " + swapiActorToPark);
    
    let parkingSpaceObjectToPost = {
        "spaceship": {
            "person": {
                "name": swapiActorToPark,
                "spaceship": null
            },
            "parkingSpace": null
        },
        "parkingLotID": 1,
        "parkingLot": null
    }

    postParkingSpace(parkingSpaceObjectToPost).then(()=>{
        alert("You have now parked!");
    });
}

//use parkingspace id to 
function unpark(parkingspaceId){
    console.log("Inside unpark function: " + parkingspaceId);

    deleteParkingSpace(parkingspaceId).then(()=>{
        alert("To unpark you have to pay: " + 5*1000 + "SW Credits. Pay?");
        alert("You have now paid and can leave. Hope to see you again!");
    });
}


function checkPerson(i){
    let inputName = $("#namebox").val()   

    //if Park is pressed
    if(i === 1){

        //Validate starwarscustomer and callback name to proceed
        validateUserFromSwapi(inputName, function(callback){
            let swapiActor = callback;
            console.log(swapiActor);

            //Check if customer has already parked
            if(swapiActor !== 0){
                getPersonRequestStatusCallback(swapiActor, function(callbackStatus){
                    console.log(callbackStatus)

                    //if found has already parked
                    if(callbackStatus === 200){
                        alert("You have already parked! Please Checkout(Unpark) before you can park again.");
                    }

                    //free to park if not found in db
                    if(callbackStatus === 404){
                        alert("Choose ship and Park!");
                        getStarShipsFromInput();
                        
                        //set global variable to star wars actor to access it when click Park
                        parkingData = swapiActor;                        
                    }
                });
            }            
        });
    } 
    
    //if Unpark is pressed
    else if(i == 2){

        //check if person has parked
        getPersonRequestStatusCallback(inputName, function(callbackStatus){
            if(callbackStatus === 404){
                alert("Are you sure you have parked? Spacepark cannot find any vehicle registred on you")
            }

            //if parked, get spaceship id
            if(callbackStatus === 200){
                getPerson(inputName).then(function(result){
                    console.log("Spaceship ID: " + result.spaceshipID);

                    //from  spaceship, get parkingspace id
                    getSpaceship(result.spaceshipID).then(function(shipResult){
                        console.log("Parkingspace ID: " + shipResult.parkingSpaceID)
                        
                        //set global variable to parkingspaceID to access it when click Unpark
                        parkingData = shipResult.parkingSpaceID;                    
                    });                    
                });
            }
        });         
    }
}

