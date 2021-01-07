
//@media only screen and(max - width: 600px) {


//    .content {
//        flex - direction: column;
//        flex - wrap: wrap;
//    }
//    .btn - half - width {
//        width: 100 %;
//        height: 50 %;

//        border - bottom - left - radius: 10 %;
//        border - top - left - radius: 10 %;
//    }
//    .allMyLotteries
//    {
//        height: 50 %;
//        width: 100 %;
//    }

function changeProperties() {


    if (numberOfLotteries == 0)
        document.getElementsByClassName('allMyLotteries').width = 0;

    if (screen.width < 600 && numberOfLotteries > 0) {
        document.getElementsByClassName('allMyLotteries').width = "100%";
        var buttonHeight = 100 / (numberOfLotteries + 1);
        var lotteriesSectionHeight = 100 - buttonHeight;
        var buttonHeightPercent = buttonHeight.toString() + "%";
        var lotteriesSectionHeightPercent = lotteriesSectionHeight.toString() + "%";
        document.getElementById("addLotterybtnId").style.height = buttonHeightPercent;
        document.getElementById("allMyLotteriesId").style.height = lotteriesSectionHeightPercent;
    }
    else if (screen.width > 600 && numberOfLotteries > 0) {
        document.getElementsByClassName('allMyLotteries').width = "50%";

  

    }

       



}
window.onresize = changeProperties;
window.onload = changeProperties;
