import * as React from 'react'
import { connect } from 'react-redux'

class SimpleIdler extends React.Component {
	componentDidMount() {
		window.document.title = 'Idler | Web UI'
	}

	render() {
		return (
			<div>
				<h1>Idler</h1>
			</div>
		)
	}
}

const Idler = connect()(SimpleIdler)

export { Idler }
