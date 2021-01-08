
function changeProperties() {


    if (numberOfLotteries == 0)
        document.getElementsByClassName('allMyLotteries').width = 0;
    else {
        

        if ($(window).width() < 600 && numberOfLotteries > 0) {
            document.getElementsByClassName('allMyLotteries').width = "100%";
            var buttonHeight = 100 / (numberOfLotteries + 1);
            var lotteriesSectionHeight = 100 - buttonHeight;
        }
        else {
            document.getElementsByClassName('allMyLotteries').width = "50%";
            var buttonHeight = 100;
            var lotteriesSectionHeight = 100;
        }
        var buttonHeightPercent = buttonHeight.toString() + "%";
        var lotteriesSectionHeightPercent = lotteriesSectionHeight.toString() + "%";
        document.getElementById("addLotterybtnId").style.height = buttonHeightPercent;
        document.getElementById("allMyLotteriesId").style.height = lotteriesSectionHeightPercent;

    }


}
window.onresize = changeProperties;
window.onload = changeProperties;


//var resizeTimer;

//$(window).on('resize', function (e) {
//    clearTimeout(resizeTimer);
//    resizeTimer = setTimeout(function () {


//    }, 250);

//});