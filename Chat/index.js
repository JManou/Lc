
const mainForm = document.getElementById("mainForm");
const mainInput = document.getElementById("mainFormInput");
const mainChatList = document.getElementById("main").getElementsByClassName("msg-list")[0];
const secondaryForm = document.getElementById("secondaryForm");
const secondaryInput = document.getElementById("secondaryFormInput");
const secondaryChatList = document.getElementById("secondary").getElementsByClassName("msg-list")[0];

mainForm.addEventListener('submit', (ev) => {
    ev.preventDefault();
    const msg = mainInput.value;
    send(msg, mainChatList, secondaryChatList);
    mainForm.reset();
})

secondaryForm.addEventListener('submit', (ev) => {
    ev.preventDefault();
    const msg = secondaryInput.value;
    send(msg, secondaryChatList, mainChatList);
    secondaryForm.reset();
})

function send(msg, sourceElement, destinationElement) {
    if(msg === '')
    {
        return;
    }

    const sentMsg = document.createElement("div");
    sentMsg.classList.add("msg-sent");
    sentMsg.innerHTML = msg;
    sourceElement.appendChild(sentMsg);
    const receiveMsg = document.createElement("div");
    receiveMsg.classList.add("msg-received");
    receiveMsg.innerHTML = msg;
    destinationElement.appendChild(receiveMsg);
    sourceElement.scrollTop  = sourceElement.scrollHeight;
    destinationElement.scrollTop  = destinationElement.scrollHeight;
}