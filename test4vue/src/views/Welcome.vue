<template>
    <div class="all">
        <h1 class="a">
            Welcome to the App
        </h1>
        <h3>
            The goal of this app is to help you price all dishes served in a restaurant in order to run an financialy helthy busines
        </h3>
        <transition name="fade">
        <h4 v-if="!isOpen">Let's <BaseButton @click="showInfo">See</BaseButton> how this works </h4>
        </transition>
        <transition name="fade">
        <div v-if="isOpen" class="info">
            <p>
            1st you need to create an account or login to one
        </p>
        <p>
            Once you are in your account you need to enter all products going in the kitchen along with their cost.
            After that, use these products to create your dishes.
            You have now a first look at how much each dish cost based on their ingredients
        </p>
        <p>
            Now you need to enter the monthly expencies of the restaurant (util bills, wages, rent etc).
            Finnaly enter an avarege number of dishes the restaurant serves in daily base.
        </p>
        <h3>
            Now based on the data provided the app will calculate for you what should be a fair price for each dish!
        </h3>
        <BaseButton @click="showInfo">Close</BaseButton>
        </div>
        </transition>
    </div>
</template>

<script>
export default {
    data() {
        return {
            isOpen: false
        }
    },
    methods: {
        showInfo(){
            this.isOpen = !this.isOpen;
        }
    },
    
}
</script>
<style scoped>
.a{
    text-align: center;
}
.all {
  background-color: lightblue;
}
h4{
    text-align: center;
}
.fade-enter{
    opacity: 0;
}
.fade-enter-active{
    transition: opacity 5s ease-out;
}
.fade-leave-to{
    opacity: 0;
}
.fade-leave-active{
    transition: opacity 5s ease-out;
}
</style>