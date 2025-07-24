document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("contactForm");
  const statusMessage = document.getElementById("statusMessage");

  form.addEventListener("submit", async (event) => {
    event.preventDefault();

    //console.log("form submitted")

    const nameInput = /** @type {HTMLInputElement} */ (document.getElementById("name"));
    const emailInput = /** @type {HTMLInputElement} */ (document.getElementById("email"));
    const messageInput = /** @type {HTMLTextAreaElement} */ (document.getElementById("message"));

    const name = nameInput.value.trim();
    const email = emailInput.value.trim();
    const message = messageInput.value.trim();

    console.log("Payload:", { name, email, message });

    if (!name || !email || !message){
      showStatus("All fields are required", false);
      return;
    }

    const payload = {name, email, message};
    try{
      const response = await fetch("https://localhost:7152/api/contact", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(payload) 
      });

      const result = await response.json();
      if(response.ok && result.success){
        showStatus(result.message, true);
        form.reset();
      } else {
        showStatus(result.message || "Something went wrong.", false);
      }
    } catch(error) {
      console.error("Fetch error", error);
      showStatus("Failed to submit. Check your network or server.", false);
    }

  });

  function showStatus(message, isSuccess){
    statusMessage.textContent = message;
    statusMessage.style.color = isSuccess ? "green" : "red";
  }
});
