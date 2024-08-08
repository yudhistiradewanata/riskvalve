const baseurl = getInputVal('#baseurl')
const urllogin = getInputVal('#urllogin')
var requestVerificationToken = $('#requestVerificationToken').val();

$("#btn-signin").prop("disabled", true).text("Loading...");
$(document).ready(function () {
  $("#btn-signin").prop("disabled", false).text("Login");
  $("#btn-signin").click(function (e) {
    e.preventDefault();
    grecaptcha.ready(function() {
        grecaptcha.execute('6LcvJSIqAAAAACnY6Gqwz6AhE9SAzrPbSPQ8meUd', {action: 'submit'}).then(function(token) {
          var username = $('input[name="username"]').val();
          var password = $('input[name="password"]').val();
          if ($(".login-form").valid()) {
            $(this).prop("disabled", true).text("Loading...");
            $.ajax({
              url: urllogin,
              type: "POST",
              headers: {
                  '__RequestVerificationToken': requestVerificationToken
              },
              data: {
                username: username,
                password: password,
                'g-recaptcha-response': token
              },
              success: function (apiresult) {
                if (apiresult.isSuccess) {
                  var data = apiresult.data;
                  var path = baseurl + "/Home/Index";
                  location.href = path;
                } else {
                  alert(apiresult.message);
                  $("#btn-signin").prop("disabled", false).text("Login");
                }
              },
            });
          }
        });
      });
  });
});
