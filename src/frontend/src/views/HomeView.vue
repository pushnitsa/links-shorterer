<template>
    <div class="d-flex pt-3 pb-2 mb-3 border-bottom">
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
