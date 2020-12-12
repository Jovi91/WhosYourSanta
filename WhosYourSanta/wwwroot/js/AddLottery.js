$('#validate').click(function () {
    $('form').validate();

    if ($('form').valid()) {
        alert("Valid");
    }
    else {
        alert("Not valid");
    }
});


var santasArray = [];
var santaDataArray = [];

function FillDataWithAdminEmail(myvar) {
    var Email = myvar;
    //var Email = '@User.Identity.Name';
    if (document.getElementById("takePart-checkbox").checked) {
        document.getElementById("SantaEmail").value = Email;
        document.getElementById("field-validation-error").textContent = "";
        document.getElementById("SantaName").value = Email.substring(0, Email.indexOf('@'));
    }
    else {
        document.getElementById("SantaEmail").value = "";
        document.getElementById("SantaName").value = "";
    }
}

function SantaAllreadyExists(item) {
    alert(item[0] + " " + item[1])
    if (item[0] == inputName) {
        alert("Mikołaj o takim nicku znajduje się już na liście");
        return true;
    }
    else if (item[1] == inputEmail) {
        alert("Mikołaj o takim mailu znajduje się już na liście");
        return true;
    }

}
// Create a "close" button and append it to each list item
//var myNodelist = document.getElementsByTagName("LI");
//var i;
//for (i = 0; i < myNodelist.length; i++) {
//    var span = document.createElement("SPAN");
//    var txt = document.createTextNode("\u00D7");
//    span.className = "close";
//    span.appendChild(txt);
//    myNodelist[i].appendChild(span);
//}

// Click on a close button to hide the current list item
var close = document.getElementsByClassName("close");
//var i;
//for (i = 0; i < close.length; i++) {
//    close[i].onclick = function () {
//        var div = this.parentElement;
//        div.style.display = "none";
//    }
//}
function AddSantaToTheList() {



    //Display santa on the list
    var li = document.createElement("li");
    inputEmail = document.getElementById("SantaEmail").value;
    inputName = document.getElementById("SantaName").value;
    var t = document.createTextNode(inputName + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + inputEmail);
    li.appendChild(t);

    //if santa data are not valid show the message and quit
    var span = document.getElementById("field-validation-error");
    var tekst = span.textContent;

    if (tekst.length > 0) {
        alert("Aby dodać mikołaja popraw dane staruszka");
        return;
    }

    

    if (inputEmail == "" || inputName == "") {
        alert("Email i nick mikołaja nie mogą być puste")
    }
    else {
        //var test = "ab";
        //var tab1 = ["11", "12"]
        //var tab2 = ["21", "22"]
        if (santasArray.length > 0) {
            //var santas = [tab1, tab2];
            //santasArray.forEach(SantaAllreadyExists(inputName,inputEmail));
            //if (santasArray.some((item) => {

            //    alert(item[0] + " " + item[1])
            //    if (item[0] == inputName) {
            //        alert("Mikołaj o takim nicku znajduje się już na liście");
            //        return true;
            //    }
            //    else if (item[1] == inputEmail) {
            //        alert("Mikołaj o takim mailu znajduje się już na liście");
            //        return true;
            //    }

            //})) { return; }

            //santasArray.some()
            if (santasArray.some(SantaAllreadyExists)) {
                document.getElementById("SantaEmail").value="";
                document.getElementById("SantaName").value="";
                return;
            }
            //santasArray.forEach(function (item) {
            //    alert(item[0] + " " + item[1])
            //    if(item[0] == inputName) {
            //        alert("Mikołaj o takim nicku znajduje się już na liście");
            //        return;
            //    }
            //    else if (item[1] == inputEmail) {
            //        alert("Mikołaj o takim mailu znajduje się już na liście");
            //        return;
            //    }
            //});

           // santas.forEach(function (entry) { test += entry[1] + " "; });
            //test += " ";
        }
        //alert(test);
        document.getElementById("EmailList").appendChild(li);
        //add santa to the array
        santaDataArray.push(inputName, inputEmail);
        santasArray.push(santaDataArray)
        santaDataArray = [];

        //santasArray.push(santaDataArray.push(inputName, inputEmail));

    
    }

    document.getElementById("SantaName").value = "";
    document.getElementById("SantaEmail").value = "";


    //create delete santa span
    var span = document.createElement("SPAN");
    var txt = document.createTextNode("\u00D7");
    span.className = "close";
    span.appendChild(txt);
    li.appendChild(span);

    for (i = 0; i < close.length; i++) {
        close[i].onclick = function () {
            var div = this.parentElement;
            div.style.display = "none";
        }
    }

    

}


function valueChanged() {
    if ($('#takePart-checkbox').is(":checked"))
        $("#AdminAsSanta").show();
    else
        $("#AdminAsSanta").hide();
}