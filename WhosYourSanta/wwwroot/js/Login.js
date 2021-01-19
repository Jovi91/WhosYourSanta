window.onload = infoAboutWebsite;

function infoAboutWebsite() {
    alert("Strona jest w trakcie tworzenia. Można się zarejestrować albo zalogować jako gość (opcja tymczasowa) aby obejrzeć co zostało zrobione. Zapraszam!")
}



function LoginAsGuest() {
    if (document.getElementById('validateIfGuest').checked) {
        document.getElementById('userEmailInput').value = "guest@wp.pl";
        document.getElementById('userPasswordInput').value = "Iamguest_1";
    }
    else
    {
        document.getElementById('userEmailInput').value = "";
        document.getElementById('userPasswordInput').value = "";
    }
}

