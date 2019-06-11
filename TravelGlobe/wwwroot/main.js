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

const addActivity = (id, position, description) => {
    return new Promise((resolve, reject) => {
        if (id && position && description) {
            fetch(`/api/positions/${id}/${position}`, {
                method: "post",
                body: JSON.stringify({ description }),
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(data => {
                if (data.status == 201) {
                    resolve();
                } else reject();
            });
        }
    });
};

const updateActivity = (id,position,activity)=>{
    return new Promise((resolve, reject) => {
        if (id && position && activity) {
            fetch(`/api/positions/${id}/${position}/${activity.id}`, {
                method: "put",
                body: JSON.stringify( activity ),
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(data => {
                if (data.status == 204) {
                    resolve();
                } else reject();
            });
        }
    });
}

new Vue({
    el: "#app",
    data: {
        name: "",
        author: "",
        id: "",
        title: "",
        positions: [],
        editable: false,
        showMajor: true,

        currentAct: null,
        currentPos:null
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

            await this.refreshPositions();
        },

        refreshPositions: async function() {
            this.positions = await getPosition();
        },

        isMajor: function(position) {
            const major = ["goodAt", "paidFor", "worldNeeds", "youLove"];
            let val = false;
            major.forEach(element => {
                if (element == position) {
                    val = true;
                }
            });
            return val;
        },

        addActivity: async function(positionName) {
            const element = document.querySelector(`#add${positionName}Text`);
            //const btn = document.querySelector(`#add${positionName}Btn`)
            //console.log(element.value);
            await addActivity(this.id, positionName, element.value);
            await this.refreshPositions();
            element.value = "";
        },

        updateActivity: async function(e) {
            await updateActivity(this.id,this.currentPos,this.currentAct)
            await this.refreshPositions()
            this.currentAct=null
            this.currentPos=null
        }
    }
});

checkCookie();
