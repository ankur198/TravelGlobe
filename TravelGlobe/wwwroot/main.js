const checkCookie = () => {
    if (
        !(
            Cookies.get("name") &&
            Cookies.get("Author") &&
            Cookies.get("ID") &&
            Cookies.get("Title")
        )
    )
        redirect("./index.html");
};

checkCookie()