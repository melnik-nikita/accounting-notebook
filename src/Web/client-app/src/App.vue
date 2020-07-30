<template>
  <div id="app">
    <b-container>
      <Title msg="Account Transactions" />
      <TransactionList></TransactionList>
    </b-container>
  </div>
</template>

<script>
import Title from "./components/Title.vue";
import TransactionList from "./components/TransactionList";

export default {
  name: "App",
  components: {
    Title,
    TransactionList,
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
