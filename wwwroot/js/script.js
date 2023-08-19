// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.getElementById("login-form").addEventListener("submit", async function (event) {
    event.preventDefault(); // Prevent form submission

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    // Create an object with the user's credentials
    const credentials = {
        email: email,
        password: password
    };

    try {
        const response = await fetch("", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(credentials)
        });

        if (response.ok) {
            const data = await response.json();
            // Assuming your API returns a token upon successful authentication
            const token = data.token;
            // Store the token securely (e.g., in localStorage or sessionStorage)
            // Redirect to the employee page or perform other actions here
            alert("Login successful!");
        } else {
            alert("Invalid email or password. Please try again.");
        }
    } catch (error) {
        console.error("Error:", error);
    }
});