﻿$('#validate').click(function () {
    $('form').validate();

    if ($('form').valid()) {
        alert("Valid");
    }
    else {
        alert("Not valid");
    }
});

function sendForm(projectId, target) {
    $.ajax({
        url: target,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            projectId: projectId,
            userAccountIds: [1, 2, 3]
        }),
        success: ajaxOnSuccess,
        error: function (jqXHR, exception) {
            alert('Error message.');
        }
    });
}

var santasArray = [];
var santaDataArray = [];
var userEmial;
function FillDataWithAdminEmail(email) {
    var userEmial = email;
    //var Email = '@User.Identity.Name';
    if (document.getElementById("takePart-checkbox").checked) {
        document.getElementById("SantaEmail").value = userEmial;
        document.getElementById("field-validation-error").textContent = "";
        document.getElementById("SantaName").value = userEmial.substring(0, userEmial.indexOf('@'));
    }
    else {
        document.getElementById("SantaEmail").value = "";
        document.getElementById("SantaName").value = "";

        //Remove Admin from santas List and Array
        var santaList = document.getElementById("SantaList");
        var listItem = santaList.getElementsByTagName("li");
        var span = santaList.getElementsByTagName("span");     
        for (var i = 0; i < listItem.length; i++) {
            var listItemContent = listItem[i].innerText;
            var spanContent = span[i].innerText;
            var emailOnTheList = (listItemContent.substring(listItemContent.indexOf("\xa0"), listItemContent.length - spanContent.length)).trim();
            if (emailOnTheList == userEmial) {
                listItem[i].remove();
                santasArray.splice(i, 1);
                return;
            }
        }
    }
}

function SantaAllreadyExists(item) {
    alert(item[1] + " " + item[2])
    if (item[1] == inputName) {
        alert("Mikołaj o takim nicku znajduje się już na liście");
        return true;
    }
    else if (item[2] == inputEmail) {
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
        if (santasArray.length > 0) {
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

            //Check if sanata is allredy in the array 
            if (santasArray.some(SantaAllreadyExists)) {
                document.getElementById("SantaEmail").value="";
                document.getElementById("SantaName").value="";
                return;
            }
        }
        document.getElementById("SantaList").appendChild(li);
        //add santa to the array
        santaDataArray.push(santasArray.length, inputName, inputEmail);
        santasArray.push(santaDataArray)
        santaDataArray = [];
    
    }

    document.getElementById("SantaName").value = "";
    document.getElementById("SantaEmail").value = "";


    //create delete santa span
    var span = document.createElement("SPAN");
    var txt = document.createTextNode("\u00D7");
    span.className = "close";
    span.appendChild(txt);
    li.appendChild(span);


    deleteListItem();
    


    //for (i = 0; i < close.length; i++) {
    //    close[i].onclick = function () {
    //        var div = this.parentElement;
    //        var listItem = div.innerText;
    //        div.style.display = "none";
    //        div.setAttribute("id", "idItemToRemove");
    //        var itemToRemove = document.getElementById("idItemToRemove");
    //        itemToRemove.remove();

    //        var santaNameToRemove = listItem.substring(0, listItem.indexOf("\xa0"));
    //        let santaIndexToRemove = santasArray.findIndex(function (el) { return el[1] == santaNameToRemove; });

    //        if (santasArray[santaIndexToRemove] ==! 'undefined')
    //            return;
    //        else
    //            santasArray.splice(santaIndexToRemove, 1);
    //    }
    //}

    

}

function deleteListItem() {
    for (i = 0; i < close.length; i++) {
        close[i].onclick = function () {
            var div = this.parentElement;
            var listItem = div.innerText;
            div.style.display = "none";
            div.setAttribute("id", "idItemToRemove");
            var itemToRemove = document.getElementById("idItemToRemove");
            itemToRemove.remove();

            var santaNameToRemove = listItem.substring(0, listItem.indexOf("\xa0"));      
            let santaIndexToRemove = santasArray.findIndex(function (el) { return el[1] == santaNameToRemove; });

            if (santasArray[santaIndexToRemove] ==! 'undefined')
                return;
            else
                santasArray.splice(santaIndexToRemove, 1);
        }
    }

}

function CreateObjects(value, index, array) {
            output += value + "</br>";
        }
    function CreateObjectFromArray() {

        output = "";

        santasArray.forEach(CreateObjects);


        //for (var obj of santasArray) {
        //    obj[key] = whatever;
        //}

        console.log(output);
    }

function valueChanged() {
    if ($('#takePart-checkbox').is(":checked"))
        $("#AdminAsSanta").show();
    else
        $("#AdminAsSanta").hide();
}