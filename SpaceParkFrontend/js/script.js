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
        if (i = 1) {
            park();
        }
        if (i = 2) {
            unpark();
        }
    });
});
