<template>
    <div class="container">
        <form @submit.prevent="onSubmit" novalidate>
            <div class="col-6 mb-4">
                <label for="inputUrl" class="form-label">Url</label>
                <input
                    type="text"
                    class="form-control"
                    :class="{ 'is-invalid': v$.fullUrl.$errors.length }"
                    id="inputUrl"
                    v-model="v$.fullUrl.$model"
                    aria-describedby="urlHelp"
                />
                <div class="invalid-feedback">Please provide a valid url.</div>
                <div id="urlHelp" class="form-text">
                    Type an Url address you want the short link to
                </div>
                <div class="mb-3">
                    <label for="inputShortLink" class="form-label">
                        Short link name (optional)
                    </label>
                    <input
                        type="text"
                        class="form-control"
                        id="inputShortLink"
                        v-model="shortName"
                        aria-describedby="shortLinkHelp"
                    />
                    <div id="shortLinkHelp" class="form-text">
                        Leave this field blank to random short link value
                    </div>
                </div>
                <button
                    :disabled="v$.$invalid"
                    type="submit"
                    class="btn btn-primary"
                >
                    Create short link
                </button>
            </div>
        </form>
    </div>
</template>

<script>
import LinksService from "@/services/LinksService";
import { required, url } from "@vuelidate/validators";
import { useVuelidate } from "@vuelidate/core";

export default {
    data() {
        return {
            fullUrl: null,
            shortName: null,
        };
    },
    setup() {
        return {
            v$: useVuelidate(),
        };
    },
    validations() {
        return {
            fullUrl: { required, url },
        };
    },
    methods: {
        onSubmit() {
            LinksService.createLink(this.fullUrl, this.shortName);
        },
    },
};
</script>
