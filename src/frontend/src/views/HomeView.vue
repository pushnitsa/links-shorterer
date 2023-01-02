<template>
    <div
        class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom"
    >
        <h1 class="h2">Home</h1>
    </div>

    <CreateShortLinkForm />
</template>

<script>
import CreateShortLinkForm from "@/components/CreateShortLinkForm.vue";

export default {
    components: { CreateShortLinkForm },
};
</script>
