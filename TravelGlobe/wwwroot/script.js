const isNameSet = () => {
    const name = Cookies.get("name");
    return name ? true : false;
};

const setName = name => {
    Cookies.set("name", name);
};

const getName = () => {
    return Cookies.get("name");
};

const getIkigiasList = () => {
    return new Promise((resolve, reject) => {
        fetch("api/Ikigais/?$Select=id,title,author")
            .then(data => data.json())
            .then(data => resolve(data));
    });
};

const createIkigias = title => {
    return new Promise((resolve, reject) => {
        if (!isNameSet()) {
            reject("Name not set");
            return;
        }

        fetch("api/Ikigais/", {
            method: "post",
            body: JSON.stringify({ title, author: getName() }),
            headers:{
                'Content-Type': 'application/json'
            }
        })
            .then(data => data.json())
            .then(data => resolve(data));
    });
};
