const baseApiURL = "http://localhost:5094/api/Contacts";

// ---------------------------Fetch All-------------------------------------

async function fetchContacts() {
    try {

        const response = await fetch(baseApiURL,{
            method:"GET"
        })

        if (!response.ok) {
            throw new Error(`Http error! Status: ${response.status}`)
        }

        const contacts = await response.json();
        console.log(contacts);
       
    } catch (error) {
        console.log("Error fetching contacts", error);
    }
} 

function displayContacts(contacts){
    const tbody = document.getElementById("list_tbody");
    tbody.innerHTML = "";

    contacts.forEach(contact => {
        const tableRow = document.createElement("tr");
        tableRow.innerHTML = `
        <td>${contact.id}</td>
        <td>${contact.name}</td>
        <td>${contact.phone}</td>
        `
        tbody.appendChild(tableRow);
    });
}

// ---------------------------Fetch One-------------------------------------

async function fetchContactById() {
    const contactId = document.getElementById("contactId").value; // Get the ID from input
    

    try {
        const response = await fetch(`${baseApiURL}/${contactId}`);
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const contact = await response.json();
        displayContact(contact); // Call display function to show the contact
    } catch (error) {
        console.error("Error fetching contact:", error);
        document.getElementById("contactDetails").innerHTML = `<p>Error: ${error.message}</p>`;
    }
}

function displayContact(contact) {
    const contactDetails = document.getElementById("contactDetails");

    if (contact) {
        contactDetails.innerHTML = `
            <h3>Contact Details:</h3>
            <p><strong>ID:</strong> ${contact.id}</p>
            <p><strong>Name:</strong> ${contact.name}</p>
            <p><strong>Phone:</strong> ${contact.phone}</p>
        `;
        contactDetails.style.display = "block"; // Make the contact details section visible
    } else {
        contactDetails.innerHTML = "<p>No contact found.</p>";
        contactDetails.style.display = "block"; // Show error message section
    }
}

// -----------------------------Create------------------------------------

document.getElementById("contactForm").addEventListener("submit", async function(event) {
    event.preventDefault();  // Prevent form from submitting normally

    // Get the input values
    const name = document.getElementById("name").value;
    const phone = document.getElementById("phone").value;

    // Prepare the data for the POST request
    const contactData = {
        name: name,
        phone: phone
    };

    try {
        // Make the POST request to the server
        const response = await fetch(`${baseApiURL}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(contactData) // Send data as JSON
        });

        if (!response.ok) {
            console.log(response);
            
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        // If successful, get the returned contact data
        const newContact = await response.json();
        
        // Optionally, show a success message or reset the form
        alert(`Contact created successfully! ID: ${newContact.id}`);
        document.getElementById("contactForm").reset();  // Reset the form

    } catch (error) {
        console.error("Error creating contact:", error);
        alert("Failed to create contact. Please try again.");
    }
});

// ----------------------------Delete---------------------------------------

document.getElementById("deleteForm").addEventListener("submit", async function(event) {
    event.preventDefault();  // Prevent form from submitting normally

    const deleteId = document.getElementById("deleteId").value;  // Get the ID from input

    try {
        // Make the DELETE request to the server
        const response = await fetch(`${baseApiURL}/${deleteId}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        // If successful, show a success message
        alert("Contact deleted successfully!");

        // Reset the form
        document.getElementById("deleteForm").reset();

    } catch (error) {
        console.error("Error deleting contact:", error);
        alert("Failed to delete contact. Please try again.");
    }
});

// --------------------------Update----------------------------------------

document.getElementById("updateForm").addEventListener("submit", async function(event) {
    event.preventDefault();  // Prevent form from submitting normally

    // Get the input values
    const contactId = document.getElementById("updateContactId").value;
    const name = document.getElementById("updateName").value;
    const phone = document.getElementById("updatePhone").value;

 
    const updatedContactData = {
        id: contactId,  // The ID is part of the data sent to update the contact
        name: name,
        phone: phone
    };

    try {
        // Make the PUT request to the server
        const response = await fetch(`${baseApiURL}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(updatedContactData) // Send data as JSON
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        // If successful, get the returned updated contact data
        const updatedContact = await response.json();
        
        // Optionally, show a success message or reset the form
        alert(`Contact with ID ${updatedContact.id} updated successfully.`);
        document.getElementById("updateForm").reset();  // Reset the form

    } catch (error) {
        console.error("Error updating contact:", error);
        alert("Failed to update contact. Please try again.");
    }
});

