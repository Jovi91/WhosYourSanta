$('#validate').click(function () {
    $('form').validate();

    if ($('form').valid()) {
        alert("Valid");
    }
    else {
        alert("Not valid");
    }
});

//function sendForm(projectId, target) {
//    $.ajax({
//        url: target,
//        type: 'POST',
//        contentType: 'application/json',
//        data: JSON.stringify({
//            projectId: projectId,
//            userAccountIds: [1, 2, 3]
//        }),
//        success: ajaxOnSuccess,
//        error: function (jqXHR, exception) {
//            alert('Error message.');
//        }
//    });
//}

var santasArray = [];
var santaDataArray = [];
var userEmial;
var santa = new Object();
var testArray = ["maslo", "braslo", "kraslo"];
function FillDataWithAdminEmail(email) {
    var userEmial = email;
    //var Email = '@User.Identity.Name';
    if (document.getElementById("takePart-checkbox").checked) {
        document.getElementById("SantaEmail").value = userEmial;
        document.getElementById("SantaEmail").disabled = true;
        document.getElementById("field-validation-error").textContent = "";
        document.getElementById("SantaName").value = userEmial.substring(0, userEmial.indexOf('@'));
    }
    else {
        document.getElementById("SantaEmail").value = "";
        document.getElementById("SantaEmail").disabled = false;
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

function SantaAllreadyExists() {
    if (santa.Name == inputName) {
        alert("Mikołaj o takim nicku znajduje się już na liście");
        return true;
    }
    else if (santa.Email == inputEmail) {
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
    //var t = document.createTextNode(inputName + '\xa0\xa0\xa0\xa0\xa0' + inputEmail);
    //li.appendChild(t);

    var t = document.createTextNode(inputName + ' \xa0\xa0\xa0\xa0\xa0 ' + inputEmail);
    var div = document.createElement("div");
    div.setAttribute("id", "santa-data");
    div.appendChild(t);
    li.appendChild(div);

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
        santa = {
            "Id": santasArray.length,
            "Name": inputName,
            "Email": inputEmail
        };
        //santaDataArray.push(santasArray.length, inputName, inputEmail);
        santasArray.push(santa)
    
    }

    document.getElementById("SantaName").value = "";
    document.getElementById("SantaEmail").value = "";
    document.getElementById("SantaEmail").disabled = false;


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

           //functions remove() and findIndex aren't supported on IE browsers so i changed it to removeChild() and some()
           //itemToRemove.remove();
           // let santaIndexToRemove = santasArray.findIndex(function (el) { return el[1] == santaNameToRemove; });
            document.getElementById("SantaList").removeChild(itemToRemove);
            var santaNameToRemove = listItem.substring(0, listItem.indexOf("\xa0"));      
           
            var index;
            let santaIndexToRemove = santasArray.some(function (e, i) {
                if (e[1] == santaNameToRemove) {
                    index = i;
                    return true;
                }
            });


            if (santasArray[santaIndexToRemove] ==! 'undefined')
                return;
            else
                santasArray.splice(santaIndexToRemove, 1);
        }
    }

}



$('#submitLotteryBtn').click(function (e) {

    //$('#lotteryNameErrorMsg').val().length;
    var invalidNameErrorLength = document.getElementById('lotteryNameErrorMsg').innerText.length;
    if ($('#lotteryNameInput').val() == "" || invalidNameErrorLength > 0) {
        alert("Wprowadź poprawną nazwę loterii!")
        return;
    }

    //Do zmiany na 4444444444444444444444444444444444444444444444444444444444444444444444444444444
    if (santasArray.length < 2) {
        alert("Loteria musi zawierać co najmniej dwóch mikołajów.")
        return;
    }

    var lotteryName = $('#lotteryNameInput').val();
    var lotteryData = {
        Name: lotteryName,
        Santas: santasArray,
        PhotoPath: 'test'
    }
    var data = JSON.stringify(lotteryData);  

    $.ajax({
        url: '/Home/AddLottery',
        type: 'post',
        contentType: 'application/json',
        data: data,
        success: function () {
            window.location.href = "/Home/Main"
        },
        error: function (errMsg) {
            alert(errMsg);
        }
    })
});


//POniższe działą
//$('#submitTest').click(function (e) {
//    var Mydata = JSON.stringify(santasArray);

//    $.ajax({
//        url: '/Home/AddLottery',
//        type: 'post',
//        contentType: 'application/json',
//        data: Mydata,
//        success: function () {
//            alert("yes");
//        },
//        error: function (errMsg) {
//            alert(errMsg);
//        }
//    })
//});
//Ale działa tylko wtedy gdy w HomeController przed parametrem metody AddLottery jest [FromBody]:
//        [HttpPost]
//        public async Task < IActionResult > AddLottery([FromBody]List < Santa > santas){ ... }
//Oraz gdy używam formatu:
//var Mydata = JSON.stringify(santasArray);  



function valueChanged() {
    if ($('#takePart-checkbox').is(":checked"))
        $("#AdminAsSanta").show();
    else
        $("#AdminAsSanta").hide();
}

//$('#lotteryNameErrorMsg').on('DOMSubtreeModified', function (e) {
   
//        alert("Hi you are authorised for this page");
//});

$("#lotteryNameErrorMsg").text('').trigger('tryThis'); 

function tryThis(){
    alert("WOOORKS???");
}

//function addDiv(param1) {
//    var content = document.getElementById('content');
//    var div;
//    alert(param1);
//        if ($(window).width() < 802 && !document.getElementById("scrollable")) {

//            div = document.createElement("div");
//            //var formHalf = document.getElementById(firstHalf);
//            //var notebookHalf = document.getElementById(SecondHalf)
//            var formHalf = document.getElementById('lotteryFormHalf');
//            var notebookHalf = document.getElementById('santaListNotebook');
//            div.setAttribute("id", "scrollable");
//            content.appendChild(div);
//            div.appendChild(formHalf);
//            div.appendChild(notebookHalf);

//            //alert($(window).width());
//        } else if (document.getElementById("scrollable") && $(window).width() >= 800) {
//            var content = $("#scrollable").contents();
//            $("#scrollable").replaceWith(content);
//        }
    

//}
//window.onresize = addDiv('lotteryFormHalf', 'santaListNotebook');
//window.onload = addDiv('lotteryFormHalf', 'santaListNotebook');
//window.addEventListener('resize', );
//$(document).$(window).resize(addDiv());

function changeLayout() {
    addDiv('lotteryFormHalf', 'santaListNotebook', 808);
}

window.onresize = changeLayout;
window.onload = changeLayout;

//window.addEventListener("resize", addDiv)

function GFG_Fun() {
    var content = $("#parent").contents();
    $("#parent").replaceWith(content);
} 
