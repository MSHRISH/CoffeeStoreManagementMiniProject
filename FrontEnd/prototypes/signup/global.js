// Array containing the two texts
const texts = [
    "Kappi Kadai",
    "காப்பி கடை"
  ];
  
  // Index to keep track of current text
  let currentIndex = 0;
  
  // Function to update the text
  function updateText() {
    document.getElementById("store-name").innerText = texts[currentIndex];
    currentIndex = (currentIndex + 1) % texts.length; // Toggle between 0 and 1
  }
  
  // Update text initially
  updateText();
  
  // Set interval to update text every 5 seconds
  setInterval(updateText, 5000);
  