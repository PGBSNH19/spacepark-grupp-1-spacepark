/****************************************************
------------------ SPACESHIP CALLS ------------------
****************************************************/

//Method making a call to our api to post a spaceship.
const postSpaceship = async(spaceshipObject) => {
    try {

        //Do request
        let response = await fetch(`https://localhost:44350/api/v1.0/spaceship`, 
            {
                method: 'POST',
                headers: {'Content-Type': `application/json`},
                body: spaceshipObject.ToJsonString()
            });

        //Log the response to console
        console.log(response);

        //Get the response body as json and return it
        let json = response.json();
        return json;
    } catch (error) {
        console.error(error);
    }
};

//Method making a call to our api to fetch a spaceship by ID.
const getSpaceship = async(id) => {
    try 
    {
        let response = await fetch
        (
            `https://localhost:44350/api/v1.0/spaceship/${id}`,
            { method: "GET" }
        );

        console.log(response);

        let json = response.json();
        return json;
    } 
    catch (error) 
    {
        console.error(error);
    }
};


/****************************************************
------------------- PERSON CALLS --------------------
****************************************************/

//Method making a call to our api to fetch a person by name.
const getParkedPerson = async(name) => 
    fetch(`https://localhost:44350/api/v1.0/person/${name}`)
    .then( response => {
        return response.json();
    })
    .catch(console.error);


    const getPerson = async(name) => {
        try {
            let response = await fetch(`https://localhost:44350/api/v1.0/person/${name}`, 
            {method: 'GET'});
            let json = response.json();
            return json;
        } catch (error) {
            console.error(error);
        }
    };
    
    //Method making a call to our api to post a person by name.
    const postPerson = async(personObject) => {
        try {
            
            //Do request
            let response = await fetch(`https://localhost:44350/api/v1.0/person`, 
            {
                method: 'POST',
                headers: {'Content-Type': `application/json`},
                body: personObject.ToJsonString()
            });
            
            //Log the response to console
            console.log(response);
            
            //Get the response body as json and return it
            let json = response.json();
            return json;
        } catch (error) {
            console.error(error);
        }
    };
    
    //Method making a call to our api to delete a person by name.
    const deletePerson = async(name) => {
        try {
            let response = await fetch(`https://localhost:44350/api/v1.0/person/${name}`, 
            {method: 'DELETE'});
            return response.json;
        } catch (error) {
            console.error(error);
        }
    };
    /****************************************************
------------------- PARKINGSPACE CALLS --------------------
****************************************************/

//Method making a call to our api to fetch a parkingspace by id.
    const GetParkingspaceByID = async(id) => 
    fetch(`https://localhost:44350/api/v1.0/ParkingSpace/${id}`)
    .then( response => {
        console.log(response)
        return response.json();
    })
    .catch(console.error);