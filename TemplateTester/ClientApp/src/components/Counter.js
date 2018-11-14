import React from 'react'
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { actionCreators } from '../store/Counter'

const Counter = props => (
	<div>
		<h1>Counter</h1>
		<p>Current count: <strong>{props.count}</strong></p>

		<button onClick={props.decrement}>Decrement</button>
		<button onClick={props.increment}>Increment</button>
	</div>
)

export default connect(
	state => state.counter,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(Counter)
