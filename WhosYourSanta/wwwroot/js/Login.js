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