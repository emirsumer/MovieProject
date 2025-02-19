var app = angular.module('MovieProjectApp', []);
app.controller('MovieProjectController', function ($scope, $http, $q) {
    $scope.BeginFunction = function () {
        alert('Welcome');
    };

    $scope.Categories = []

  

});
$scope.Login = function () {

    var userData = {
        KullaniciAdi: $scope.TxtUserName,
        Sifre: $scope.TxtPassword
    }

    $http.post('~/Login/Login', JSON.stringify(userData)).then(function (response) {
        if (response.status === 200) {
            //alert("Başarılı");
            WriteCookie("MovieToken", response.data, 20);
            var Cookie = GetCookie("MovieToken");
            alert(Cookie);
        }
    }, function (response) {
        if (response.status !== 200) {
            Swal.fire({
                icon: 'error',
                title: 'Hata',
                text: 'Hata kullanıcı adı ya da şifre',
            });
        }
        else {
            $scope.msg = "Service not Exists";
            $scope.statusval = response.status;
            $scope.statustext = response.statusText;
            $scope.headers = response.headers();
        }
    });





    $scope.GetCategories = function () {
        $http.post('~/Category/Categories').then(function (response) {
            if (response.status === 200) {
                alert(response.data)
                if (response.data.value !== null) {
                    $scope.Categories.response.data.value;
                }
            })
    }, function (response) {
        if (response.status !== 200) {
            Swal.fire({
                icon: 'error',
                title: 'Hata',
                text: 'Kategorileri getirirken hata meydana geldi',
            });
        }
    }


};

$scope.GetCategories = function () {
    $http.get('/Category/Categories').then(function (response) {
        if (response.status === 200) {
            if (response.data.value !== null) {
                var Categories = response.data.value;   
            }
        }
    }, function (response) {
        if (response.status !== 200) {
            Swal.fire({
                icon: 'error',
                title: 'Hata',
                text: 'Kategorileri getirirken hata meydana geldi',
            });
        }
    });
};

function WriteCookie(CookieName, CookieValue, ExMinutes) {
    const d = new Date();
    d.setTime(d.getTime() + (ExMinutes * 24 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = CookieName + "=" + CookieValue + ";" + expires + ";path=/";
}

function GetCookie(CookieName) {
    let name = CookieName + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}