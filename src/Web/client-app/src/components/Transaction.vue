<template>
  <b-card no-body class="mb-1">
    <b-card-header header-tag="header" class="p-1" role="tab">
      <b-button
        block
        v-b-toggle:[transaction.id]
        :variant="getButtonType"
      >{{transaction.type}} - {{transaction.amount}}</b-button>
    </b-card-header>
    <b-collapse :id="transaction.id" accordion="my-accordion" role="tabpanel">
      <b-card-body>
        <b-card-text>
          <span class="bold">Id</span>
          : {{transaction.id}}
        </b-card-text>
        <b-card-text>
          <span class="bold">Type</span>
          : {{transaction.type}}
        </b-card-text>
        <b-card-text>
          <span class="bold">Amount</span>
          : {{transaction.amount}}
        </b-card-text>
        <b-card-text>
          <span class="bold">EffectiveDate</span>
          : {{transaction.effectiveDate}}
        </b-card-text>
      </b-card-body>
    </b-collapse>
  </b-card>
</template>

<script>
export default {
  props: {
    transaction: Object,
  },
  computed: {
    getButtonType() {
      let type = "success";
      switch (this.transaction.type.toLowerCase()) {
        case "credit":
          type = "success";
          break;
        case "debit":
          type = "danger";
          break;
      }
      return type;
    },
  },
};
</script>

<style scoped>
.bold {
  font-weight: 600;
}
</style>