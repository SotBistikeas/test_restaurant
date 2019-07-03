<!--
<BaseSelect label="SELECT_OTI_THELOUME" :options="OTI_THELOUME" v-model="event.OTI_THELOUME"/>
-->
<template>
	<div>
		<label v-if="label"> {{ label }} </label>
		<select :value="value" @input="updateValue" v-bind="$attrs" v-on="listeners">	
			<option v-for="option in options" :key="option" :selected="option === value"> 
				{{ option }} 
			</option>
		</select>
	</div>
</template>

<script> 
 export default {
 	inheritAttrs: false,
 	props: {
 		options:{type:Array,required: true},
 		label: {type: String,default:''},
		value:[String, Number]
	},
	computed:{
		listeners(){
			return {...this.$listeners,input: this.updateValue}
		}
	},
 	methods:{
		 updateValue(product) {
			 this.$emit('input', product.target.value)
			 }
		}
};
</script>

