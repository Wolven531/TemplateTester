import * as React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { actionCreators } from '../store/Idler'

class SimpleIdler extends React.Component {
	componentDidMount() {
		window.document.title = 'Idler | Web UI'
	}

	render() {
		return (
			<article>
				<h1>Idler</h1>
				<section>
					<h2>Resources: {this.props.numResource}</h2>
				</section>
				<section>
					<h2>Workers: {this.props.numWorkers}</h2>
				</section>
				<section>
					<button onClick={this.props.generateResource}>Generate Resource</button>
				</section>
			</article>
		)
	}
}

const Idler = connect(
	state => state.idler,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(SimpleIdler)

export { Idler }
