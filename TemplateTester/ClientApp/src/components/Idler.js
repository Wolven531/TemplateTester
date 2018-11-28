import * as React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import {
	COST_WORKER,
	actionCreators
} from '../store/Idler'

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
					<button onClick={this.props.generateResource}>Generate Resource (+1)</button>
				</section>
				<section>
					<button disabled={this.props.numResource < COST_WORKER}
						onClick={this.handleBuyWorkerClick}>Buy Worker (Cost={COST_WORKER})</button>
				</section>
			</article>
		)
	}

	handleBuyWorkerClick = (evt) => {
		if (this.props.numResource < COST_WORKER) {
			return
		}
		this.props.buyWorker()
	}
}

const Idler = connect(
	state => state.idler,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(SimpleIdler)

export { Idler }
