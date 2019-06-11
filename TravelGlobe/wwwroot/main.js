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

const getPosition = () => {
    return new Promise((resolve, reject) => {
        fetch(`/api/positions/${Cookies.get("ID")}`)
            .then(data => data.json())
            .then(data => resolve(data));
    });
};

new Vue({
    el: "#app",
    data: {
        name: "",
        author: "",
        id: "",
        title: "",
        positions: [],
        editable: false,
        showMajor: true
    },
    mounted() {
        this.initData();
    },
    methods: {
        initData: async function() {
            this.name = Cookies.get("name");
            this.author = Cookies.get("Author");
            this.id = Cookies.get("ID");
            this.title = Cookies.get("Title");
            this.editable = this.name === this.author;

            this.positions = await getPosition();
        },

        isMajor: function(position) {
            const major = ["goodAt", "paidFor", "worldNeeds", "youLove"];
            let val = false;
            major.forEach(element => {
                if (element == position) {
                    console.log(element,element == position);
                    val= true;
                }
            });
            return val;
        }
    }
});

checkCookie();
