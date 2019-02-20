//prefixes of implementation that we want to test
window.indexedDB = window.indexedDB || window.mozIndexedDB || window.webkitIndexedDB || window.msIndexedDB;

//prefixes of window.IDB objects
window.IDBTransaction = window.IDBTransaction || window.webkitIDBTransaction || window.msIDBTransaction;
window.IDBKeyRange = window.IDBKeyRange || window.webkitIDBKeyRange || window.msIDBKeyRange

if (!window.indexedDB) {
  window.alert("Your browser doesn't support a stable version of IndexedDB.")
}
if (window.indexedDB){
  console.log("yo exeis indexed database....");
}
const employeeData = [
];
var db;
var request = window.indexedDB.open("newDatabase", 4);

request.onerror = function(event) {
  console.log("error: ");
};

request.onsuccess = function(event) {
  db = request.result;
  console.log("success: "+ db);
};

request.onupgradeneeded = function(event) {
  var db = event.target.result;
  var objectStore = db.createObjectStore("employee", {keyPath: "id"});

  for (var i in employeeData) {
    objectStore.add(employeeData[i]);
  }
}


new Vue ({
  el: '#app',
  data(){
    return {
      employee:{
        id: null,
        name: null,
        age: 0,
        email: null
      },
      employees: [],
      retryCount: 0,

    }
  },
  mounted(){
    this.readAll()
  },

  methods:{
    add(employee) {
      var vm = this
      let request = new Promise((resolve, reject) => {
        var request = db.transaction(["employee"], "readwrite")
        .objectStore("employee").add({ 
          id: employee.id != null ? employee.id : vm.employees.length++ ,
          name: employee.name, 
          age: employee.age, 
          email:employee.email 
        })
        request.onsuccess = function(event) {
          vm.employees.push({ 
            edit: false,
            id: employee.id != null ? vm.employee.id : vm.employees.length++ ,
            name: employee.name, 
            age: employee.age, 
            email: employee.email 
          })
        }
        request.onerror = function(event) {
          console.log("Unable to add data ");
        }
      }); 
    },
    clear() {
      var vm = this
      var request = db.transaction(["employee"], "readwrite")
      .objectStore("employee")
      .clear()
      vm.employees = []
    },
    edit(employee, index) {

      var vm = this 
      new Promise((resolve, reject) => {
        vm.remove(employee.id, index)
        resolve(vm.add(employee)); 
      });

    },
    readAll() {
      var vm = this 
      try {
        vm.employees = []
        var objectStore = db.transaction("employee").objectStore("employee");
        if(objectStore){
          objectStore.openCursor().onsuccess = function(event) {
            var cursor = event.target.result;

            if (cursor) {
              if(vm.employees){
                vm.employees.push({ edit: false,  id: cursor.key , name: cursor.value.name , age:cursor.value.age, email:cursor.value.email});

              }
              cursor.continue();

            }
          };
        }
      }catch(e){
        vm.retryDisp()
      }
    },
    remove(id, index) {
      var vm = this 
      var request =  db.transaction(["employee"], "readwrite")
      .objectStore("employee")
      .delete(id);
      vm.employees.splice(index, 1)
    },
    retryDisp() {
      var vm = this
      if( ++vm.retryCount > 5 ) {
        console.log('Cannot open the database after 5 retries');
        vm.readAll();
      }
      setTimeout(function(){ vm.readAll() }, 100);
    }
  }
})