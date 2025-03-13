const baseUrl = 'http://localhost:5094/api';

function formSubmitted(event) {
    event.preventDefault();

    const formData = new FormData();

    var userLogin = document.getElementById('login').value;
    var userPassword = document.getElementById('password').value;

    formData.append('login', userLogin);
    formData.append('password', userPassword);

    //Sending a post request using fetch:

    fetch(`${baseUrl}/Auth/login`, {
        method: "POST",
        body: formData
    })

        .then(async response => {
            if (!response.ok) {
                const errorMessage = await response.text(); // Get error message from server
                throw new Error(`Error ${response.status}: ${errorMessage}`);
            }
            return response.json(); // Parse JSON response if status is 200
        })
        .then(data => {
            localStorage.setItem("accessToken", data.accessToken); // Store token
            Swal.fire({
                title: "Good job!",
                text: "Successful log in ",
                icon: "success"
            });
        })
        .catch(error => {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: error.message,
            });
        });
}

function checkToken() {
    var token = localStorage.getItem('accessToken');

    if (token === null || token.length <= 0)
     //Swal alert icon:error
       return;

    var tokenPayloadEncoded = token.split('.')[1];

    // Decode the String
    var tokenPayloadDecodedString = atob(tokenPayloadEncoded);
    var payload = JSON.parse(tokenPayloadDecodedString);
    console.log(payload.role);
}

function getContacts() {
    const token = localStorage.getItem("accessToken");  // Retrieve the token
    fetch(`${baseUrl}/Contacts`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`,  // ✅ Attach token
            "Content-Type": "application/json"
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error(`HTTP Error! Status: ${response.status}`);
        }
        return response.json();
    })
    .then(data => {
        console.log("Contacts:", data);
        
    })
    .catch(error => {
        console.error("Error fetching contacts:", error);
    });
}
