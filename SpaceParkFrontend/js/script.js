

document.getElementById('submit').onclick=function(){GetPersonfromSwapi()}

/*
function GetPersnFromApi()
{
    let name=$("#namebox").val();

    let conectionString = `https://swapi.dev/api/people/?search=${name}`;
    console.log(conectionString);
    fetch(conectionString)
    .then(res => res.json())
    .then(data => {
        
        if(data.count==1)
        {
            
            GetShipForPersonFromApi(data.results[0].starships);
          $("#errormessage").text(`you have successfully parked here ${name}  `)

      }
      else if(data.count!=0)
      {
          $("#errormessage").text(`Pleas typ in your full name `)
            //här måste man välja vilken person man är 
      }
      else
      {
          $("#errormessage").text(`no person whit the name ${name} in the database`)
          console.log("no person whit that name in the database")
      }

    });
    
    
}
*/

function GetPersonfromSwapi()
{
    let name = document.getElementById('namebox').value;
   alert(name)
    fetch(`https://swapi.dev/api/people/?search=${name}`).then(res=>res.json()).then(data=> console.log(data));

   
}
