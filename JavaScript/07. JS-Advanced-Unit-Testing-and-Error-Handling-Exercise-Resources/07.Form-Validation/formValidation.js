function validate() {
  const userNameRegex = /(^[A-z0-9]{3,20}$)/;
  const passwordRegex = /^[\w]{5,15}$/;
  const emailRegex = /^[^@.]+@[^@]*\.[^@]*$/;
  const companyNumberRegex = /^[0-9]{4}$/;

  const companyInformation = document.getElementById("companyInfo");
  const companyChekbox = document.getElementById("company");
  const submitButton = document.getElementById("submit");

  const userNameField = document.getElementById("username");
  const passwordField = document.getElementById("password");
  const confirmPasswordField = document.getElementById("confirm-password");
  const emailField = document.getElementById("email");
  const companyNumberField = document.getElementById("companyNumber");
  const validDiv = document.getElementById("valid");

  submitButton.addEventListener("click", (ev) => {
    ev.preventDefault();

    let isValidInput = true;

    if (!userNameRegex.test(userNameField.value)) {
      isValidInput = false;
      userNameField.style.borderColor = "red";
    } else {
      userNameField.style.borderColor = "";
    }

    if (
      !passwordRegex.test(passwordField.value) ||
      passwordField.value != confirmPasswordField.value
    ) {
      isValidInput = false;
      passwordField.style.borderColor = "red";
      confirmPasswordField.style.borderColor = "red";
    } else {
      passwordField.style.borderColor = "";
      confirmPasswordField.style.borderColor = "";
    }

    if (!emailRegex.test(emailField.value)) {
      isValidInput = false;
      emailField.style.borderColor = "red";
    } else {
      emailField.style.borderColor = "";
    }

    if (
      companyChekbox.checked &&
      !companyNumberRegex.test(companyNumberField.value)
    ) {
      isValidInput = false;
      companyNumberField.style.borderColor = "red";
    } else {
      companyNumberField.style.borderColor = "";
    }

    if (isValidInput) {
      validDiv.style.display = "block";
    } else {
      validDiv.style.display = "none";
    }
  });

  companyChekbox.addEventListener("change", (ev) => {
    if (ev.target.checked) {
      companyInformation.style.display = "block";
    } else {
      companyInformation.style.display = "none";
    }
  });
}
