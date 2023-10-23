function prepareFormForSubmit() {
    const actorListInput = document.getElementById('ActorList');
    const actorList = document.getElementById('actorListShow');
    const actors = [];

    // Lặp qua từng thẻ <li> trong danh sách
    actorList.querySelectorAll('li').forEach(li => {
        // Tạo một phần tử div tạm thời để chứa nội dung của thẻ <li>
        const tempDiv = document.createElement('div');
        tempDiv.appendChild(li.cloneNode(true)); // Sao chép nội dung của <li> vào div tạm thời

        // Loại bỏ nội dung của thẻ <button> trong div tạm thời
        const buttonElement = tempDiv.querySelector('span');
        if (buttonElement) {
            buttonElement.remove();
        }

        // Lấy nội dung đã loại bỏ thẻ <button> và làm sạch nó
        const actorName = tempDiv.textContent.trim();

        // Thêm nội dung vào mảng actors
        actors.push(actorName);
    });

    // Hiển thị danh sách các nội dung đã lấy được
    console.log(actors);


        // Set the value of the hidden input to the JSON representation of actors
        actorListInput.value = JSON.stringify(actors);
    }


function addActor() {
    const actorInput = document.getElementById('actor');
    const actorName = actorInput.value.trim();

    if (actorName !== '') {
        // Create a new <li> element
        const listItem = document.createElement('li');
        listItem.textContent = actorName;

        // Create a delete button for the actor
        const deleteButton = document.createElement('span');
        deleteButton.className = 'remove-actor';
        deleteButton.textContent = 'X';

        deleteButton.addEventListener('click', () => {
            removeActor(listItem);
        });

        listItem.appendChild(deleteButton);

        // Append the <li> element to the actorList
        const actorList = document.getElementById('actorListShow');
        actorList.appendChild(listItem);

        // Clear the actorInput
        actorInput.value = '';

        // Update the hidden input for form submission
        prepareFormForSubmit();
    }
}

function removeActor(actorElement) {
    const actorList = document.getElementById('actorListShow');

    // Remove the actor's <li> element from the actorList
    actorList.removeChild(actorElement);

    // Update the hidden input for form submission
    prepareFormForSubmit();
}


// Function to display the list of actors
function displayActors() {
    const actorList = document.getElementById('actorListShow');
    actorList.innerHTML = '';

    addedActors.forEach(actor => {
        const listItem = document.createElement('li');
        listItem.textContent = actor;

        const deleteButton = document.createElement('button');
        deleteButton.className = 'delete-button';
        deleteButton.textContent = 'X';

        deleteButton.addEventListener('click', () => {
            removeActor(actor);
        });

        listItem.appendChild(deleteButton);
        actorList.appendChild(listItem);
    });
}

// Function to check if a video URL is valid
function isValidVideoUrl(url) {
    const videoUrlPattern = /^(http(s)?:\/\/)?[\w.-]+\.\w+(\/\S*)?$/;
    return videoUrlPattern.test(url);
}

// Function to update the video iframe
function updateVideo() {
    const videoUrl = document.getElementById("directory").value;
    const iframeElement = document.getElementById("video_frame");

    if (isValidVideoUrl(videoUrl)) {
        iframeElement.src = videoUrl;
    } else {
        iframeElement.src = "";
    }
}

// Function to display an image
function displayImage() {
    const imageUrl = document.getElementById("image_link").value;
    const imageElement = document.getElementById("image_display");
    imageElement.src = imageUrl;
}
// Gọi hàm prepareFormForSubmit() khi trang HTML đã được load xong
document.addEventListener("DOMContentLoaded", function () {
    prepareFormForSubmit();
    displayImage();
    updateVideo();
});

// Rest of your JavaScript code
// ...
