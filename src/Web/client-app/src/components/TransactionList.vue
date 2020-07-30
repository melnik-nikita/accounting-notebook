<template>
  <div role="tablist">
    <Transaction v-for="tran in transactions" :key="tran.id" v-bind:transaction="tran"></Transaction>
  </div>
</template>

<script>
import Transaction from "./Transaction";

export default {
  data() {
    return {
      transactions: [],
    };
  },
  created() {
    this.fetchTransactions();
  },
  methods: {
    async fetchTransactions() {
      const response = await this.$http.get(
        "http://localhost:5000/api/transactions"
      );

      var data = await response.json();

      this.transactions = data;
    },
  },
  components: {
    Transaction,
  },
};
</script>